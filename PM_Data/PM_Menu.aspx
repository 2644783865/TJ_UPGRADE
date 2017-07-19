<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Menu.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu" class="easyui-layout">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 50; i++) {
                if (document.getElementById("HyperLink" + i))
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num))
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }

    </script>

    <asp:Timer ID="Timer1" Interval="60000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="任务分工及制作明细" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                生产任务分工<asp:Label ID="lblassign" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblfengong" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink24" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        我的任务<asp:Label ID="lblmytask" runat="server" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HiddenField ID="hf_name" runat="server" />
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                制作明细管理<asp:Label runat="server" ID="zzmxgl" ForeColor="Red"></asp:Label>
                                <asp:Label ID="zzmx_ck_bt" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink32" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        油漆方案管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink35" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        工艺类卡片
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink36" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        通用类卡片
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink42" onClick="SelectMenu(42);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_SCDYTZDGL.aspx" Target="right" runat="server">
                    <p>
                        生产代用通知
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink43" onClick="SelectMenu(43);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_SCDYTZSP.aspx" Target="right" runat="server">
                    <p>
                        生产代用通知管理
                        <asp:Label runat="server" ID="lbDYTZ" Text="" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink28" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                计划单变更通知<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink30" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                代用单审核<asp:Label ID="lb_dydsh" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lb_mp_ck" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
            </div>
            <div title="生产外协管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        生产外协制定
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        生产外协汇总
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    Target="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                生产外协审批<asp:Label ID="lbl_wxsp" runat="server" ForeColor="Red"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        生产外协比价<asp:Label ID="wxbj" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                外协比价审批<asp:Label ID="lbl_wxbjsp" runat="server" ForeColor="Red"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink37" onClick="SelectMenu(37);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        外协订单管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink38" onClick="SelectMenu(38);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        外协结算单管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink33" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                我的报检任务<asp:Label ID="lb_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink39" onClick="SelectMenu(39);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        外协进度管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink29" Visible="false" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>委外合同</p></asp:HyperLink>
            </div>
            <div title="成品管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FaHuoNotice.aspx" Target="right">
                    <p>
                        发货通知<asp:Label runat="server" ID="fhtz" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        成品库存管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        成品入库管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink19" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                成品入库审批<asp:Label ID="lbl_finish" runat="server" ForeColor="Red"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        成品发运比价<asp:Label ID="lbl_fayun" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink17" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                发运比价审批<asp:Label ID="lbl_fysp" runat="server" ForeColor="Red"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink40" onClick="SelectMenu(40);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        发运费用均摊
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        成品支领-出库管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                成品支领-出库审批<asp:Label ID="lbl_out" runat="server" ForeColor="Red"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
            </div>
            <div title="刀具库管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        刀具库存管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        刀具入库管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        刀具出库管理
                    </p>
                </asp:HyperLink>
            </div>
            <div title="成本统计管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                        生产工时管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                       生产成本统计
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink27" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                       班组物料结算
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink26" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                       厂内分包
                    </p>
                </asp:HyperLink>
            </div>
            <div title="其他管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <p>
                       项目计划管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink41" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_Kaipiao_List.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                开票管理<asp:Label runat="server" ID="lblKaiPiao" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink18" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                不合格品通知单<asp:Label ID="lb_rejectPro" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
        <p> 新增采购申请 </p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                采购申请审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                
                <asp:HyperLink ID="HyperLink44" onClick="SelectMenu(44);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                需用计划驳回
                                <asp:Label ID="lblBack" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink31" runat="server" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ServiceList.aspx?Dep=04" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                顾客服务处理<asp:Label runat="server" ID="fwsq" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink34" runat="server" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_CUSTOMER.aspx" Target="right"><p>顾客财产</p></asp:HyperLink>
                 
                <asp:HyperLink ID="HyperLink45" runat="server" onClick="SelectMenu(45);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/TM_Data/TM_GSFPMANAGEMENT.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel45" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                工时数据统计</p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(46);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_contract_task_view.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                项目综合信息查询</p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
