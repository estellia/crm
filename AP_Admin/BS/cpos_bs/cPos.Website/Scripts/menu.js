function getUrlParam(strname) {
    var hrefstr,pos,parastr,para,tempstr;
    hrefstr = window.location.href;
    pos = hrefstr.indexOf("?");
    parastr = hrefstr.substring(pos+1);
    para = parastr.split("&");
    tempstr="";
    for(i=0;i<para.length;i++) {
        tempstr = para[i];
        pos = tempstr.indexOf("=");
        if(tempstr.substring(0,pos) == strname) {
            return tempstr.substring(pos+1);
        }
    }
    return null;
}
var sysMenuIdList = [];
function loadSysMenus() {
    var sysMenusCtrl = document.getElementById("sysMenus");
    if (sysMenusCtrl != null) {
        sysMenusCtrl.innerHTML = "加载中...";
        sysMenuIdList = [];
        MenuService.getFirstLevelMenus(loadSysMenus_callback);
    }
}
function loadSysMenus_callback(result) {
    var sysMenusCtrl = document.getElementById("sysMenus");
    sysMenusCtrl.innerHTML = "";
    if (result != null) {
        for (var i = 0; i < result.length; i++) {
            var subItem = result[i];
            if (subItem.Status != 1) {
                continue;
            }
            var subItemCtrl = document.createElement("li");
            subItemCtrl.id = "sysMenu_0_" + subItem.Menu_Id;
            subItemCtrl.rootMenuId = subItem.Menu_Id;
            sysMenusCtrl.appendChild(subItemCtrl);
            sysMenuIdList.push(subItemCtrl.id);
            var str = "<a href=\"#\" onclick=\"expendSysMenu(true, '" + subItem.Menu_Id +
                "', 0);return false;\">" + subItem.Menu_Name + "</a>";
            subItemCtrl.innerHTML = str;
            var subItemLoadCtrl = document.createElement("div");
            subItemLoadCtrl.id = "sysMenu_0_" + subItem.Menu_Id + "_L";
            subItemLoadCtrl.innerHTML = "";
            subItemLoadCtrl.className = "lab_d";
            subItemLoadCtrl.style.display = "none";
            sysMenusCtrl.appendChild(subItemLoadCtrl);
        }
    }
    focusSysMenu();
}
function expendSysMenu(enableSet, menuId, level, menuId2, menuId3) {
    if (level == 2) return;
    hideSysMenuOthers(menuId, level);
    var menuLoadCtrl = document.getElementById(
        "sysMenu_" + level + "_" + menuId + "_L");
    if (menuLoadCtrl.style.display == "") {
        menuLoadCtrl.style.display = "none"; return;
    }
    menuLoadCtrl.style.display = "";
    if (menuLoadCtrl.innerHTML != "") return;
    menuLoadCtrl.innerHTML = "加载中...";
    MenuService.getSubMenus(menuId, expendSysMenu_callback, null, 
        [ enableSet, menuId, level, menuId2, menuId3] );
}
function expendSysMenu_callback(result, context) {
    if (context == null) return;
    var enableSet = context[0];
    var menuId = context[1];
    var level = parseInt(context[2]);
    var menuId2 = context[3];
    var menuId3 = context[4];
    var subLevel = parseInt(context[2]) + 1;
    var menuLoadCtrl = document.getElementById(
        "sysMenu_" + level + "_" + menuId + "_L");
    menuLoadCtrl.innerHTML = "";
    if (result != null) {
        for (var i = 0; i < result.length; i++) {
            var subItem = result[i];
            var subItemCtrl = document.createElement("div");
            subItemCtrl.id = "sysMenu_" + subLevel + "_" + subItem.Menu_Id;
            if (subLevel == 1) {
                subItemCtrl.rootMenuId = subItem.Parent_Menu_Id;
                subItemCtrl.Parent_Menu_Id = subItem.Parent_Menu_Id;
            }
            if (subLevel == 2) {
                subItemCtrl.className = "l_2";
                var pEl = document.getElementById("sysMenu_1_" + subItem.Parent_Menu_Id);
                subItemCtrl.rootMenuId = pEl.rootMenuId;
                subItemCtrl.Parent_Menu_Id = subItem.Parent_Menu_Id;
            }
            else subItemCtrl.className = "l_1";
            menuLoadCtrl.appendChild(subItemCtrl);
            sysMenuIdList.push(subItemCtrl.id);
            var url = subItem.URLPathWithID == null || 
                subItem.URLPathWithID == undefined ? "#" : subItem.URLPathWithID;
            var str = "";
            var actionScript = "openSysMenu('" + subItem.Menu_Id + "', '" + subItem.Parent_Menu_Id + 
                "', '" + subLevel + "', '" + url + "');return false;";
            if (subLevel == 2) str = "<a href=\"" + url + "\" onclick=\"" + actionScript + "\">" + subItem.Menu_Name + "</a>";
            else str = "<a href=\"#\" onclick=\"expendSysMenu(true, '" + subItem.Menu_Id +
                    "', '" + subLevel + "');return false;\">" + subItem.Menu_Name + "</a>";
            subItemCtrl.innerHTML = str;
            var subItemLoadCtrl = document.createElement("div");
            subItemLoadCtrl.id = "sysMenu_" + subLevel + "_" + subItem.Menu_Id + "_L";
            subItemLoadCtrl.innerHTML = "";
            subItemLoadCtrl.style.display = "none";
            menuLoadCtrl.appendChild(subItemLoadCtrl);
            var focusItemId = getUrlParam("cur_menu_id");
            if (focusItemId == subItem.ID && subLevel == 2) {
                subItemCtrl.className = "l_3";
            }
            if (menuId2 != undefined && menuId2 != null && menuId2 == subItem.Menu_Id)
                expendSysMenu(false, menuId2, subLevel, menuId3);
        }
    }
}
function openSysMenu(menuId, parentMenuId, level, url) {
    //location.href = url;
    $("#mainframe")[0].src = url;
}
function hideSysMenuOthers(menuId, level) {
    if (sysMenuIdList == null) return;
    for (var i = 0; i < sysMenuIdList.length; i++) {
        var context = sysMenuIdList[i].split("_");
        if (context[1] == level && context[2] != menuId) {
            var otherLoadCtrl = document.getElementById(sysMenuIdList[i] + "_L");
            otherLoadCtrl.style.display = "none";
        }
    }
}
function focusSysMenu() {
    var menuId = getUrlParam("cur_menu_id");
    MenuService.getMenuFullPath(menuId, focusSysMenu_callback, null, []);
}
function focusSysMenu_callback(result, context) {
    if (result != null) {
        var menuIds = result;
        if (menuIds == null) return;
        var menuIdsArray = menuIds.split("_");
        var menuId1 = menuIdsArray[0];
        var menuId2 = menuIdsArray[1];
        var menuId3 = menuIdsArray[2];
        if (menuId1 == undefined || menuId1 == null || menuId1 == "") return;
        var ctrl = document.getElementById("sysMenu_0_" + menuId1);
        if (ctrl != undefined && ctrl != null)
            expendSysMenu(false, menuId1, 0, menuId2, menuId3);
    }
}
function showMenu() {
    var divMenu = document.getElementById("divMenu");
    var imgShowMenu = document.getElementById("imgShowMenu");
    if (divMenu != null && imgShowMenu != null) {
        if (imgShowMenu.src.endsWith("img/dot_f.png")) {
            //divMenu.style.display = "hidden";
            divMenu.style.display = "none";
            imgShowMenu.src = "../img/dot_fz.png";
        }
        else {
            divMenu.style.visibility = "visible";
            divMenu.style.display = "block";
            imgShowMenu.src = "../img/dot_f.png";
        }
    }
}
