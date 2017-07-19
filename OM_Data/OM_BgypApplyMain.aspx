<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_BgypApplyMain.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BgypApplyMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品申请&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <%--  <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("确定将所选定项目下推生成入库单？");
            return retVal;
        }
    </script>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner" style="width: 100%">
        <div class="box_right">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <table width="100%">
                <tr>
                    <td style="width: 26%" align="left">
                        申请时间：<asp:TextBox runat="server" ID="txt_starttime" class="easyui-datebox" editable="false"
                            Width="100px" Height="18px"></asp:TextBox>到
                        <asp:TextBox runat="server" ID="txt_endtime" class="easyui-datebox" editable="false"
                            Width="100px" Height="18px"></asp:TextBox>
                    </td>
                    <td style="width: 50%" align="left">
                        办公用品名称：
                        <asp:TextBox ID="txtName" runat="server" Text="" Width="100px" Height="15px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        规格或参数：
                        <asp:TextBox ID="txtModel" runat="server" Text="" Width="100px" Height="15px"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_OnClick" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDaochu" runat="server" Text="导 出" OnClick="btnDaochu_OnClick" />
                    </td>
                    <td style="width: 20%">
                        实领总额：
                        <asp:Label ID="lab_count" Text="" runat="Server" />
                        本部门额度:
                        <asp:Label ID="lblEDU" Text="" runat="Server" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left" width="60px">
                        审核状态：
                    </td>
                    <td width="400px">
                        <asp:RadioButtonList runat="server" ID="rblState" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_click"
                            RepeatColumns="5">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                            <asp:ListItem Text="驳回" Value="1"></asp:ListItem>
                            <asp:ListItem Text="通过" Value="2"></asp:ListItem>
                            <asp:ListItem Text="我的审核任务" Value="3" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="ckbChange" runat="server" Text="以旧换新" AutoPostBack="true" OnCheckedChanged="ckbChange_CheckedChanged" />
                    </td>
                    <td align="left">
                        申请部门：<asp:DropDownList ID="ddl_bumen" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbumen_click">
                            <%-- <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                        <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="驳回" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="通过" Value="2"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <%--<asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_BgypApply.aspx?action=add"
                            runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                ImageAlign="AbsMiddle" runat="server" />
                            添加申请
                        </asp:HyperLink>--%>
                        <%--   <asp:Button ID="btnAdd" OnClick="btnAdd_onclick" runat="Server" Text="添加申请" />--%>
                        <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_BgypApply.aspx?action=add"
                            runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                ImageAlign="AbsMiddle" runat="server" />
                            添加申请
                        </asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <%--<td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
                                </td>--%>
                </tr>
            </table>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-wrapper" style="width: 100%">
                <div class="box-outer" style="width: 100%; overflow: scroll">
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>使用申请单号</strong>
                                    </td>
                                    <td>
                                        <strong>编码</strong>
                                    </td>
                                    <td>
                                        <strong>名称</strong>
                                    </td>
                                    <td>
                                        <strong>规格或参数</strong>
                                    </td>
                                    <td>
                                        <strong>单价</strong>
                                    </td>
                                    <td>
                                        <strong>申请数量</strong>
                                    </td>
                                    <td>
                                        <strong>金额</strong>
                                    </td>
                                    <td>
                                        <strong>库存数量</strong>
                                    </td>
                                    <td>
                                        <strong>申请人</strong>
                                    </td>
                                    <td>
                                        <strong>审核人</strong>
                                    </td>
                                    <td>
                                        <strong>审核状态</strong>
                                    </td>
                                    <td>
                                        <strong>审核意见</strong>
                                    </td>
                                    <td>
                                        <strong>申请部门</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                    <td>
                                        <strong>申请时间</strong>
                                    </td>
                                    <td>
                                        <strong>实领数</strong>
                                    </td>
                                    <td>
                                        <strong>实领金额</strong>
                                    </td>
                                    <td>
                                        <strong>添加实领数</strong>
                                    </td>
                                    <td>
                                        查看
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label ID="CODE" runat="server" Text='<%#Eval("CODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLBM" runat="server" Text='<%#Eval("WLBM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLNAME" runat="server" Text='<%#Eval("WLNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLMODEL" runat="server" Text='<%#Eval("WLMODEL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLPRICE" runat="server" Text='<%#Eval("WLPRICE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLNUM" runat="server" Text='<%#Eval("WLNUM")%>'></asp:Label>
                                        <td>
                                            <asp:Label ID="WLJE" runat="server" Text='<%#Eval("WLJE")%>'></asp:Label>
                                        </td>
                                    </td>
                                    <td>
                                        <asp:Label ID="num" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="APPLY" runat="server" Text='<%#Eval("APPLY")%>'></asp:Label><asp:Label
                                            ID="APPLYID" runat="server" Visible="false" Text='<%#Eval("APPLYID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="REVIEW" runat="server" Text='<%#Eval("REVIEW")%>'></asp:Label><asp:Label
                                            ID="REVIEWID" runat="server" Visible="false" Text='<%#Eval("REVIEWID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("REVIEWSTATE").ToString().Contains("0") ? "未审核" : Eval("REVIEWSTATE").ToString().Contains("1") ?"驳回":"通过"%>
                                    </td>
                                    <td>
                                        <asp:Label ID="REVIEWSTATE" runat="server"></asp:Label>
                                        <asp:HyperLink ID="hlTask1" CssClass="link" runat="server" Visible="false">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Label ID="DEPNAME" runat="server" Text='<%#Eval("DEPNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLNOTE" runat="server" Text='<%#Eval("WLNOTE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="DATE" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="WLSLS" runat="server" Text='<%#Eval("WLSLS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="GET_MONEY" runat="server" Text='<%#Eval("GET_MONEY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="link_bh" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_BgypApply.aspx?action=addsls&id="+Eval("CODE") %>'>
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            添加实领数</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" CssClass="link" runat="server" NavigateUrl='<%#"OM_BgypApply.aspx?action=view&id="+Eval("CODE") %>'>
                                            <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />查看
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="5" align="right">
                                合计:
                            </td>
                            <td>
                            </td>
                            <td align="center">
                                <asp:Label runat="server" ID="lblAppNum"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label runat="server" ID="lblAppMoney"></asp:Label>
                            </td>
                            <td colspan="8">
                            </td>
                            <td align="center">
                                <asp:Label runat="server" ID="lblSlsNum"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label runat="server" ID="lblSlsMoney"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有相关物料信息!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
