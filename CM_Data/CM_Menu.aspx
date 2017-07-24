<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CM_Menu.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Menu" %>

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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 50; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }

        //        setInterval(function() {
        //            var p = $("#tishi")[0];
        //            var title = p.innerText;
        //            var first = title.substr(0, 1);
        //            var last = title.substr(1);
        //            p.innerText = last + first;
        //            $("#tishi").css("color", "red");
        //        }, 500);
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="60000">
    </asp:Timer>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项" data-options="collapsible:false">
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="市场任务管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink4" runat="server" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_PJinfo.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                经营计划单<asp:Label runat="server" ID="jyjhd" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink1" runat="server" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_TaskEdit.aspx" Target="right"><p>计划单变更</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink9" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                计划单变更评审<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" runat="server" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ZengBuPs.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                计划单增补评审<asp:Label runat="server" ID="zbtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_JHDQXSP.aspx" Target="right" runat="server">
                    <p>
                        计划单取消评审
                        <asp:Label runat="server" ID="lbQXPS" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
            </div>
            <div title="市场合同管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>合同审批</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                合同审批任务<asp:Label ID="MyViewTask" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>销售合同</p></asp:HyperLink>
                    
                <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(46);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_contract_task_view.aspx" Target="right">
                            <p>项目综合信息查询</p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_HTBGTZDGL.aspx" Target="right" runat="server">
                    <p>
                        合同变更通知单<asp:Label runat="server" ID="lbHTBGTZD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
            </div>
            <div title="市场顾客管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink18" runat="server" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHLXDGL.aspx" Target="right" >
                    <p>
                        顾客服务联系单
                        <asp:Label runat="server" ID="lbLXD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink19" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLDGL.aspx" Target="right" Visible="false">
                    <p>
                        质量问题处理单
                        <asp:Label runat="server" ID="lbCLD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                 <asp:HyperLink ID="HyperLink37" runat="server" onClick="SelectMenu(37);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_CX.aspx" Target="right">
                    <p>
                        质量处理单查询
                        <asp:Label runat="server" ID="Label2" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink32" runat="server" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_ADD.aspx" Target="right">
                    <p>
                        新增质量处理单
                        <asp:Label runat="server" ID="Label1" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink31" runat="server" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_SP.aspx" Target="right">
                    <p>
                        质量处理单审批
                        <asp:Label runat="server" ID="lbCLD_SP" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink27" runat="server" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_FG.aspx" Target="right">
                    <p>
                        质量处理单分工
                        <asp:Label runat="server" ID="lbCLD_FG" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink30" runat="server" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_YYFX.aspx" Target="right">
                    <p>
                        填写原因分析
                        <asp:Label runat="server" ID="lbCLD_YYFX" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink33" runat="server" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_TXCLYJ.aspx" Target="right">
                    <p>
                        填写处理意见
                        <asp:Label runat="server" ID="lbCLD_CLYJ" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink34" runat="server" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_TXCLFA.aspx" Target="right">
                    <p>
                        填写处理方案
                        <asp:Label runat="server" ID="lbCLD_CLFA" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink35" runat="server" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_TXCLJG.aspx" Target="right">
                    <p>
                        填写处理过程及结果
                        <asp:Label runat="server" ID="lbCLD_CLJG" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink36" runat="server" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLD_TXFWFY.aspx" Target="right">
                    <p>
                        统计服务费用
                        <asp:Label runat="server" ID="lbTJ" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
            </div>
            <div title="其他管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);"
                    CssClass="LeftMenuNoSelected" NavigateUrl="~/CM_Data/CM_PinSPerson.aspx" Target="right"><p>评审人员设置</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/PD_DocManage.aspx" Target="right"><p>投标评审</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_CUSTOMER.aspx" Target="right"><p>顾客财产</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" runat="server" Visible="false" onClick="SelectMenu(6);"
                    CssClass="LeftMenuNoSelected" NavigateUrl="~/CM_Data/CM_ServiceList.aspx?" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                顾客服务申请<asp:Label runat="server" ID="fwsq" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" runat="server" Visible="false" onClick="SelectMenu(7);"
                    CssClass="LeftMenuNoSelected" NavigateUrl="~/CM_Data/CM_ServiceFile.aspx" Target="right"><p>顾客服务文件</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_FHList.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p id="tishi">
                                发货通知<asp:Label runat="server" ID="fhtz" ForeColor="Red" Visible="true"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink11" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_Kaipiao_List.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                开票管理<asp:Label runat="server" ID="lblKaiPiao" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_GZLXDGL.aspx" Target="right" runat="server">
                    <p>
                        工作联系单<asp:Label runat="server" ID="lbGZLXD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_TZTHTZDGL.aspx" Target="right" runat="server">
                    <p>
                        图纸替换通知单<asp:Label runat="server" ID="lbTZTHTZD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <%--<asp:HyperLink ID="HyperLink17" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
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
                </asp:HyperLink>--%>
            </div>
            <div title="成品库管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_Finished_LIST.aspx" Target="right" runat="server">
                    <p>
                        成品库存管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink24" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_Finished_IN.aspx" Target="right" runat="server">
                    <p>
                        成品入库管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FINISHED_IN_Audit.aspx" Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
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
                <asp:HyperLink ID="HyperLink28" onClick="SelectMenu(28);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_Finished_OUT.aspx" Target="right" runat="server">
                    <p>
                        成品支领-出库管理
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink29" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FINISHED_OUT_Audit.aspx" Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
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
            <div title="发运管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink16" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FaHuoNotice.aspx" Target="right">
                    <p>
                        发货通知<asp:Label runat="server" ID="lbFHTZ1" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink26" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FHList.aspx" Target="right" runat="server">
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
                    NavigateUrl="~/PM_Data/PM_CPFYJSDGL.aspx" Target="right" runat="server">
                    <p>
                        发运费用均摊
                    </p>
                </asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
