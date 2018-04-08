Ext.define('Jit.form.Label', {
    extend: 'Ext.form.Label'
    , alias: 'widget.jitlabel'
    , config: {
        /*
        @size 可选的值有small,big
        */
        jitSize: null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , margin: '0 10 10 10'          
            , height: 22
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 183;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 233;
                    }
                    break;
            }
        }      
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    jitSetValue: function (value) {
        this.setText(value);
    }
});