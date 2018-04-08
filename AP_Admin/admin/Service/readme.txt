1.编译选项 
1.1 是否需要将管理平台的数据交换至业务平台(SYN_CP)
    在发布给外包方时，由于环境问题，不需要在Project的Properties的Build/General/Conditional compilation symbols中加入该编译选项;
    在公司内进行测试时，需要加入该编译选项；
	在正式发布时，需要加入该编译选项；
1.2 在与业务平台交换数据时，是否对数据进行加密(DATA_ENCRYPT)
    在单元测试时，不需要在Project的Properties的Build/General/Conditional compilation symbols中加入该编译选项;
    在综合测试时，需要加入该编译选项，以测试功能是否正常；
	在正式发布时，需要加入该编译选项，以保证数据在传输过程中的安全；
