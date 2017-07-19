<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHLXDGL.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHLXDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    服务联系单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
    
    
        function trClick(obj) {
            window.open("CM_SHLXD.aspx?action=read&id=" + obj);
        }

        function btnReset_OnClientClick() {
            $("#<%=txtLXD_GKMC.ClientID%>").val("");
            $("#<%=txtLXD_BH.ClientID%>").val("");
            $("#<%=txtLXD_HTH.ClientID%>").val("");
            $("#<%=txtLXD_RWH.ClientID%>").val("");
            $("#<%=txtLXD_XMMC.ClientID%>").val("");
            $("#<%=txtLXD_SBMC.ClientID%>").val("");
            $("#<%=txtLXD_ZDR.ClientID%>").val("");
            $("#<%=txtLXD_ZDSJ.ClientID%>").val("");
        }

        function btnDelete_OnClientClick() {
            if (confirm("确实要删除吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function btnRefuse_OnClientClick() {
            if (confirm("确实要驳回吗？驳回后单子表单将回到初始状态")) {
                return true;
            }
            else {
                return false;
            }
        }
        
        $(function() {
            $("#tab tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        })
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title" align="right">
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="我的任务" Value="2" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            审批状态：
                            <asp:DropDownList runat="server" ID="ddlSPZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                                <asp:ListItem Text="未通过" Value="5"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnQT" Text="其他筛选" BackColor="#00ffcc" />
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnQT"
                                PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                CancelControlID="btnClose" Y="80">
                            </asp:ModalPopupExtender>
                        </td>
                        <td align="right">
                            <asp:Button runat="server" ID="btnDaoChu" Text="导出" OnClick="btnDaoChu_onclick" Width="30px"
                                Height="25px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnDelete" Text="删除" OnClientClick="return btnDelete_OnClientClick()"
                                OnClick="btnDelete_OnClick" Width="30px" Height="25px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnAlter" Text="修改" OnClick="btnAlter_OnClick" Width="30px"
                                Height="25px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnRefuse" Text="驳回" Visible="false" OnClientClick="return btnRefuse_OnClientClick()"
                                OnClick="btnRefuse_OnClick" Width="30px" Height="25px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnAdd" Text="新增服务联系单" OnClick="btnAdd_OnClick"
                                Height="25px" BackColor="#71cf37" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelCondition" runat="server" Width="75%" Style="display: none">
        <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
            <tr>
                <td colspan="7" align="center">
                    <asp:Button ID="QueryButton" runat="server" OnClick="Query" Text="查询" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClientClick="btnReset_OnClientClick()" />&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <input type="button" id="btnClose" value="关闭" />
                </td>
            </tr>
            <tr>
                <td>
                    按顾客名称查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_GKMC"></asp:TextBox>
                </td>
                <td>
                    按编号查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_BH"></asp:TextBox>
                </td>
                <td>
                    按合同号查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_HTH"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    按任务号查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_RWH"></asp:TextBox>
                </td>
                <td>
                    按项目名称查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                </td>
                <td>
                    按设备名称查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    按项目名称查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_XMMC"></asp:TextBox>
                </td>
                <td>
                    按设备名称查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_SBMC"></asp:TextBox>
                </td>
                <td>
                    按制单人查询
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_ZDR"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    按制单日期
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLXD_ZDSJ"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater runat="server" ID="rptSHFWLXD" OnItemDataBound="rptSHFWLXD_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <th>
                                        <strong>序号</strong>
                                    </th>
                                    <th>
                                        <strong>顾客名称</strong>
                                    </th>
                                    <th>
                                        <strong>编号</strong>
                                    </th>
                                    <th>
                                        <strong>合同号</strong>
                                    </th>
                                    <th>
                                        <strong>任务号</strong>
                                    </th>
                                    <th>
                                        <strong>项目名称</strong>
                                    </th>
                                    <th>
                                        <strong>设备名称</strong>
                                    </th>
                                    <th>
                                        <strong>信息简介</strong>
                                    </th>
                                    <th>
                                        <strong>服务结果</strong>
                                    </th>
                                    <th>
                                        <strong>服务总费用</strong>
                                    </th>
                                    <th>
                                        <strong>制单人</strong>
                                    </th>
                                    <th>
                                        <strong>制单日期</strong>
                                    </th>
                                    <th>
                                        <strong>负责部门</strong>
                                    </th>
                                    <th>
                                        <strong>审批状态</strong>
                                    </th>
                                    <th>
                                        <strong>审批</strong>
                                    </th>
                                    <th>
                                        <strong>填写服务结果</strong>
                                    </th>
                                    <th>
                                        <strong>统计服务费用</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                    ondblclick='trClick(<%#Eval("LXD_ID") %>)'>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label runat="server" ID="XuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:Label runat="server" ID="LXD_ID" Text='<%#Eval("LXD_ID") %>' Visible="false"></asp:Label>
                                        <%--<asp:Label runat="server" ID="LXD_SPZT" Text='<%#Eval("LXD_SPZT")%>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="LXD_SPLX" Text='<%#Eval("LXD_SPLX")%>' Visible="false"></asp:Label>--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_GKMC" Text='<%#Eval("LXD_GKMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_BH" Text='<%#Eval("LXD_BH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_HTH" Text='<%#Eval("LXD_HTH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_RWH" Text='<%#Eval("LXD_RWH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_XMMC" Text='<%#Eval("LXD_XMMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_SBMC" Text='<%#Eval("LXD_SBMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="LXD_XXJJ" Text='<%#Eval("LXD_XXJJ")%>' ToolTip='<%#Eval("LXD_XXJJ")%>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="LXD_FWGC" Text='<%#Eval("LXD_FWGC") %>' ToolTip='<%#Eval("LXD_FWGC") %>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_FWZFY" Text='<%#Eval("LXD_FWZFY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_ZDR" Text='<%#Eval("LXD_ZDR")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_ZDSJ" Text='<%#Eval("LXD_ZDSJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_FZBM" Text='<%#Eval("LXD_FZBM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LXD_SPZT1" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplSP" NavigateUrl='<%#"CM_SHLXD.aspx?action=check&id="+Eval("LXD_ID")%>'>
                                            <asp:Image runat="server" ID="imgCheck" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/images/shenhe.gif" />
                                            审批
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTX" NavigateUrl='<%#"CM_SHLXD.aspx?action=write&id="+Eval("LXD_ID")%>'>
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTJ" NavigateUrl='<%#"CM_SHLXD.aspx?action=count&id="+Eval("LXD_ID")%>'>
                                            <asp:Image runat="server" ID="Image2" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            统计
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
