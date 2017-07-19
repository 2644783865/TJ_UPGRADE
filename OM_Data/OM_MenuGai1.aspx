<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_MenuGai1.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_MenuGai1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/OM_menu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .back
        {
            font-size: 12px;
            display: block;
            background: url(images/menunoselected.gif) no-repeat;
            cursor: pointer;
            margin-top: 10px;
            margin-bottom: 10px;
        }
    </style>
</head>
<body id="leftMenu" class="easyui-layout">

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 200; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="3000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div id="div_rl" title="人力资源管理" runat="server" style="overflow: auto; background-color: #E3F1FA;
                border-width: 2px;">
                <div id="menuContent1" class="easyui-accordion" fit="true">
                    <div title="&nbsp 人员基础信息管理" selected="false" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink1" runat="server" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/sta_detail.aspx" Target="right">
                         <p>&nbsp 基础信息管理与查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink136" runat="server" onClick="SelectMenu(136);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/staff_record.aspx" Target="right">
                            <p>&nbsp 历史信息查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink94" runat="server" onClick="SelectMenu(94);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ContractRecord.aspx" Target="right">
                            <p>&nbsp 合同签订记录</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink95" runat="server" onClick="SelectMenu(95);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_StaffChange.aspx" Target="right">
                            <p>&nbsp 人员增减统计</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 考勤管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink32" runat="server" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTJ.aspx" Target="right">
                        <p style="width:180px">&nbsp &nbsp 考勤汇总与查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink110" runat="server" onClick="SelectMenu(110);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTJdetail.aspx" Target="right">
                        <p style="width:180px">&nbsp &nbsp 跨月考勤明细</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink112" runat="server" onClick="SelectMenu(112)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ZYKQTJdetail.aspx" Target="right">
                            <p>&nbsp &nbsp 整月考勤明细</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink33" runat="server" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTOTAL.aspx" Target="right">
                    <p>&nbsp &nbsp  历史考勤统计</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink106" runat="server" onClick="SelectMenu(106);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Person_Hol.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 带薪年假管理<asp:Label ID="lbbingjiatx" runat="server" ForeColor="Red" ToolTip="当年累计病事假已达到20天人数，点击进入查看！"></asp:Label></asp:Label><asp:Label
                                    ID="lbqingltx" runat="server" ForeColor="Red" ToolTip="最近需要清零！"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div id="div1" runat="server" title="&nbsp &nbsp 薪酬管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZQD.aspx" Target="right">
                    <p>&nbsp &nbsp  工资表</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink79" runat="server" onClick="SelectMenu(79);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/MULTIHZEXPORT.aspx" Target="right">
                    <p>&nbsp &nbsp 工资数据综合查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink92" runat="server" onClick="SelectMenu(92);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Salarysearch.aspx" Target="right">
                    <p>&nbsp &nbsp 工资汇总查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink26" runat="server" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHe_JXGZ_List.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 职能部门绩效工资<asp:Label ID="lblDepJXGZ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink23" runat="server" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_bzAverageList.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 一线班组平均工资<asp:Label ID="lblBZEverage" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink12" runat="server" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SCCZSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 班组绩效工资<asp:Label ID="labelczgw" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink66" runat="server" onClick="SelectMenu(66);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXADDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 人员绩效审批<asp:Label ID="lbjxaddsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink72" runat="server" onClick="SelectMenu(72);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXGZYESP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 绩效工资结余审批<asp:Label ID="lbjxgzyesp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink77" runat="server" onClick="SelectMenu(77);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXGZSYSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 绩效工资使用审批<asp:Label ID="lbjxgzsysp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZ.aspx" Target="right">
                    <p>&nbsp &nbsp  岗位工资台账</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink76" runat="server" onClick="SelectMenu(76);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 岗位工资审批<asp:Label ID="lbgdgzsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink82" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZSCSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 岗位工资删除审批<asp:Label ID="lbgdgzscsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        <asp:HyperLink ID="HyperLink99" runat="server" onClick="SelectMenu(99);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SALARYBASEDATA.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 薪酬基数台账</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink100" runat="server" onClick="SelectMenu(100);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SALARYBASEDATASP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 薪酬基数审批<asp:Label ID="lbxcxssp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        
                        <asp:HyperLink ID="HyperLink59" runat="server" onClick="SelectMenu(59);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZYDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 薪酬异动审批<asp:Label ID="lbgzydsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink24" runat="server" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZQDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 工资审批<asp:Label ID="lbgzqd" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink34" runat="server" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZT.aspx" Target="right" Visible="false">
                    <p>&nbsp &nbsp  工资条</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 餐补管理" selected="false" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink111" runat="server" onClick="SelectMenu(111)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBu.aspx" Target="right">
                             <p>&nbsp &nbsp 餐补数据管理 </p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink75" runat="server" onClick="SelectMenu(75)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBuSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 餐补数据审批
                                <asp:Label ID="lbcanbusp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink91" runat="server" onClick="SelectMenu(91)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBuYDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 餐补异动审批
                                <asp:Label ID="lbcanbuydsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 员工关系管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHBX.aspx" Target="right">
                    <p>&nbsp &nbsp  员工社险管理</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GJJ.aspx" Target="right">
                    <p>&nbsp &nbsp  员工公积金管理</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink4" runat="server" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LDBX.aspx" Target="right">
                    <p> &nbsp &nbsp 派遣员工社险公积金</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink62" runat="server" onClick="SelectMenu(62);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_XUNISHBX.aspx" Target="right">
                    <p> &nbsp &nbsp 重机虚拟户社险</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink63" runat="server" onClick="SelectMenu(63);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_XUNIGJJ.aspx" Target="right">
                    <p> &nbsp &nbsp 重机虚拟户公积金</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RenYuanDiaoDongMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 人员调动审批表<asp:Label ID="lblMove" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink135" runat="server" onClick="SelectMenu(135);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LZSQJSX.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 离职手续办理
                                        <asp:Label runat="server" ID="lbLZSX" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 绩效管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink11" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeMuBList.aspx" Target="right">
                    <p>&nbsp &nbsp 绩效模板管理</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink13" runat="server" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeList.aspx" Target="right">
                    <p>&nbsp &nbsp 启动考核项</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink14" runat="server" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeAudit.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 考核人评价项<asp:Label ID="lblKaohe" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink6" runat="server" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoheRecord.aspx" Target="right">
                    <p>&nbsp &nbsp 员工绩效月度成绩</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink25" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeList_DepartMonth.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 组织绩效月度成绩<asp:Label ID="lblDepartMonth" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 培训管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink42" runat="server" onClick="SelectMenu(42);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDPXJH_GL.aspx" Target="right">
                            <p>
                                &nbsp 年度培训计划申请
                                <asp:Label runat="server" ID="lbNDPX_SQ" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink15" runat="server" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDPXJJH_HZSP.aspx" Target="right">
                            <p>
                                &nbsp 年度培训计划汇总<asp:Label runat="server" ID="lbNDPX_HZ" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink29" runat="server" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSPX_GL.aspx" Target="right">
                            <p>
                                &nbsp 临时培训计划<asp:Label runat="server" ID="lbLSPX" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink22" runat="server" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXBM_GL.aspx" Target="right">
                    <p>&nbsp &nbsp 培训报名</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink21" runat="server" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JDPXJH.aspx" Target="right">
                    <p>&nbsp &nbsp 培训实施</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink44" runat="server" onClick="SelectMenu(44);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXDA.aspx" Target="right">
                    <p> &nbsp &nbsp  个人培训档案</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink45" runat="server" onClick="SelectMenu(45);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXJSDA.aspx" Target="right">
                    <p>&nbsp &nbsp  培训讲师档案</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink43" runat="server" onClick="SelectMenu(43);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXXQ_GL.aspx" Target="right">
                    <p>&nbsp &nbsp 培训需求调查</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink78" runat="server" onClick="SelectMenu(78);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXJBXS.aspx" Target="right">
                    <p>&nbsp &nbsp 培训学时统计</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 招聘配置管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink39" runat="server" onClick="SelectMenu(39);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_GL.aspx" Target="right">
                    <p>&nbsp &nbsp  年度招聘计划申请</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink65" runat="server" onClick="SelectMenu(65);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDZPJH_HZGL.aspx" Target="right">
                    <p>&nbsp &nbsp  年度招聘计划汇总</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink20" runat="server" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 年度招聘计划审批<asp:Label runat="server" ID="lbZPJH" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink67" runat="server" onClick="SelectMenu(67);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_GL.aspx" Target="right">
                    <p>&nbsp &nbsp  临时招聘计划申请</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink68" runat="server" onClick="SelectMenu(68);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_HZGL.aspx" Target="right">
                    <p>&nbsp &nbsp  临时招聘计划汇总</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink69" runat="server" onClick="SelectMenu(69);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 临时招聘计划审批<asp:Label runat="server" ID="lbLSZPJH" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink36" runat="server" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_CL.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 年度招聘计划处理<asp:Label runat="server" ID="Label1" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink40" runat="server" onClick="SelectMenu(40);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYPZQD.aspx" Target="right">
                    <p>&nbsp &nbsp 人员配置清单</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink37" runat="server" onClick="SelectMenu(37);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYPZQD_JL.aspx" Target="right">
                    <p>&nbsp &nbsp 人员配置历史查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink41" runat="server" onClick="SelectMenu(41);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="" Target="right" Visible="false">
                    <p> &nbsp &nbsp  测评表格</p>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
            <div title="行政管理" style="overflow: auto; background-color: #E3F1FA; border-width: 1px;">
                <div id="menuContent7" class="easyui-accordion" fit="true">
                    <div title="&nbsp &nbsp 车辆管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink56" runat="server" onClick="SelectMenu(56);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarApplyAudit.aspx" Target="right"><p>&nbsp &nbsp  用车审核</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink48" runat="server" onClick="SelectMenu(48);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarApply.aspx" Target="right"><p>&nbsp &nbsp  用车申请</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(46);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Car.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车辆档案<asp:Label ID="lblCarBX" runat="server" Text="" ForeColor="Red"></asp:Label><asp:Label
                                    ID="lblCarWH" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink47" runat="server" onClick="SelectMenu(47);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarRecord.aspx" Target="right"><p>&nbsp &nbsp  行车记录查询</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink57" runat="server" onClick="SelectMenu(57);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarWeixiuShenqing.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 维修/保养申请<asp:Label runat="server" Text="" ForeColor="Red" ID="lbWXSQ"></asp:Label><asp:Label
                                    runat="server" Text="" ForeColor="Red" ID="lbBYSQ"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink54" runat="server" onClick="SelectMenu(54);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarWeihu.aspx" Target="right"><p>&nbsp &nbsp  维修/保养/加油</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink55" runat="server" onClick="SelectMenu(55);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHENHE.aspx" Target="right"><p>&nbsp &nbsp  审核流程配置</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink30" runat="server" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Driver.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 司机档案<asp:Label ID="lblSJ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink85" runat="server" onClick="SelectMenu(78);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarKuCun.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车品库存<asp:Label ID="lbclkc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink86" runat="server" onClick="SelectMenu(79);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARRK.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车品入库<asp:Label ID="lbclrk" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink87" runat="server" onClick="SelectMenu(80);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARRK_SP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车品入库审批<asp:Label ID="lbclrksp" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink88" runat="server" onClick="SelectMenu(81);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARCK.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车品出库<asp:Label ID="lbclck" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink89" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARCK_SP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 车品出库审批<asp:Label ID="lbclcksp" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 固定资产管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink49" runat="server" onClick="SelectMenu(49);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcPcPlan.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 固定资产采购<asp:Label ID="lblGDZCpc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink50" runat="server" onClick="SelectMenu(50);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcStore.aspx" Target="right"><p>&nbsp &nbsp  固定资产库存</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink51" runat="server" onClick="SelectMenu(51);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcIn.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 固定资产入库<asp:Label ID="lblGDZCIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink70" runat="server" onClick="SelectMenu(70);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDZCRK_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 固定资产入库审批<asp:Label runat="server" ID="lbGDZCRK" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink52" runat="server" onClick="SelectMenu(52);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcTransSum.aspx" Target="right"><p>&nbsp &nbsp  固定资产转移记录</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink71" runat="server" onClick="SelectMenu(71);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDZCZY_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 固定资产转移审批<asp:Label runat="server" ID="lbGDZCZY" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink83" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcBaofeiSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 固定资产报废审批<asp:Label runat="server" ID="lbGDZCBF" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink90" runat="server" onClick="SelectMenu(90);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ZCKX.aspx" Target="right"><p>&nbsp &nbsp  资产查询</p></asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 办公设备资产管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink35" runat="server" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcPcPlan.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 办公设备资产采购<asp:Label ID="lblFGDZCpc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink38" runat="server" onClick="SelectMenu(38);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcStore.aspx" Target="right"><p>&nbsp &nbsp  办公设备资产库存</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink58" runat="server" onClick="SelectMenu(58);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcIn.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 办公设备资产入库<asp:Label ID="lblFGDZCIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink73" runat="server" onClick="SelectMenu(73);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGDZCRK_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 办公设备资产入库审批<asp:Label runat="server" ID="lbFGDZCRK" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink60" runat="server" onClick="SelectMenu(60);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcTransSum.aspx" Target="right"><p>&nbsp &nbsp  办公设备资产转移</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink74" runat="server" onClick="SelectMenu(74);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGDZCZY_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 办公设备资产转移审批<asp:Label runat="server" ID="lbFGDZCZY" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink84" runat="server" onClick="SelectMenu(84);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcBaofeiSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 办公设备资产报废审批<asp:Label runat="server" ID="lbFGDZCBF" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 办公用品管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink18" runat="server" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypApplyMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 使用申请<asp:Label ID="lblBGYPSQ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink19" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypPcApplyMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 采购申请<asp:Label ID="lblBGYPPC" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink96" runat="server" onClick="SelectMenu(96);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypChangeRecord.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 以旧换新查询</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink81" runat="server" onClick="SelectMenu(81);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_PCHZ.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 办公用品汇总审批<asp:Label ID="lblBGYPHZSP" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink16" runat="server" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_In_List.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 入库管理<asp:Label ID="lblBGYPIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink17" runat="server" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_Store.aspx" Target="right"><p>&nbsp &nbsp 库存查询</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink27" runat="server" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Bgyp_DingE.aspx" Target="right"><p>&nbsp &nbsp  超额情况查询</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink10" runat="server" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_SafeStore.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 办公用品安全库存<asp:Label ID="lblSafe" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 住宿管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink53" runat="server" onClick="SelectMenu(53);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SUSHE.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 住宿管理</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink61" runat="server" onClick="SelectMenu(61);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHUIDFLIST.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 住宿水电费管理</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink80" runat="server" onClick="SelectMenu(80);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHUIDFSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp 住宿水电费审批<asp:Label runat="server" ID="lbZSSDFSP" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 食堂管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink9" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Eat.aspx" Target="right"><p>&nbsp &nbsp 申请用餐(仅供查看)</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink31" runat="server" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_EATNEWLIST.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 申请用餐(新)<asp:Label ID="lbsqycnew" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 差旅管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink93" runat="server" onClick="SelectMenu(93);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_TravelApply.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 差旅申请
                                <asp:Label ID="lbTravelApply" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink97" runat="server" onClick="SelectMenu(97);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_TravelDelay.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 差旅延期
                                <asp:Label ID="lbTravelDelay" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp 其他行政管理" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink28" runat="server" onClick="SelectMenu(28);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_YongYinList.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 用印管理<asp:Label ID="lblYY" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink64" runat="server" onClick="SelectMenu(64);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ComputerLIst.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 电子设备报修<asp:Label ID="lblComputer" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink98" runat="server" onClick="SelectMenu(98);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ExpressManage.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 快递管理<asp:Label ID="lbExpress" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        <asp:HyperLink ID="HyperLink101" runat="server" onClick="SelectMenu(101);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/jiPiaomanagementaspx.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 机票管理<asp:Label ID="lbjipiao" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink102" runat="server" onClick="SelectMenu(102);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/PowerAuditManagement.aspx" Target="right">
                            <p>
                                &nbsp &nbsp 权限审批管理<asp:Label ID="lbpower" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
