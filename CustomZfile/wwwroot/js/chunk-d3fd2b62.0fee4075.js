(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-d3fd2b62"],{"152a":function(a,t,e){"use strict";e.r(t);var n=function(){var a=this,t=a.$createElement,e=a._self._c||t;return e("div",{staticClass:"monitor-body"},[e("h1",[e("span",[a._v("系统监控")]),e("el-button",{staticStyle:{float:"right"},attrs:{type:"primary",icon:"el-icon-refresh",size:"medium"},on:{click:a.loadData}},[a._v("刷新")])],1),e("el-table",{attrs:{data:a.tableData}},[e("el-table-column",{attrs:{prop:"key",label:"属性",width:"300"}}),e("el-table-column",{attrs:{prop:"value",label:"值"}})],1)],1)},l=[],o={name:"Monitor",data:function(){return{info:{},tableData:[],timer:null}},methods:{loadData:function(){var a=this;this.$http.get("admin/monitor").then((function(t){a.info=t.data.data,a.tableData=[],a.tableData.push({key:"服务器总空间",value:a.info.totalSpace+" GB"}),a.tableData.push({key:"可用空间剩余",value:a.info.freeSpace+" GB"}),a.tableData.push({key:"服务器名",value:a.info.osName}),a.tableData.push({key:"操作系统",value:a.info.osDesciption}),a.tableData.push({key:"CPU 架构",value:a.info.machineArch}),a.tableData.push({key:"系统启动时间",value:a.info.upTime}),a.tableData.push({key:"系统总内存",value:a.info.totalMemory+" GB"}),a.tableData.push({key:"系统空闲内存",value:a.info.freeMemory+" GB"}),a.tableData.push({key:"CLR 版本",value:a.info.clrVersion})}))}},mounted:function(){this.loadData()}},i=o,u=(e("afac"),e("2877")),s=Object(u["a"])(i,n,l,!1,null,"622ea7fa",null);t["default"]=s.exports},afac:function(a,t,e){"use strict";var n=e("fe56"),l=e.n(n);l.a},fe56:function(a,t,e){}}]);