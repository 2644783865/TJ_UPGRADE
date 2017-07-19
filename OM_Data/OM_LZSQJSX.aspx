<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_LZSQJSX.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LZSQJSX" Title="辞职申请及离职手续办理" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    辞职申请及离职手续办理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function trClick(a) {
            window.open('OM_LZSXBLD.aspx?action=read&id=' + a);
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td align="right" style="text-align: right">
                        <asp:HyperLink runat="server" ID="lbtnFSLZSX" NavigateUrl="~/OM_Data/OM_LZSXBLD.aspx?action=add"
                            Target="_blank">
                            <asp:Image border="0" hspace="2" ImageAlign="AbsMiddle" Style="cursor: hand" runat="server"
                                ID="image1" ImageUrl="~/Assets/images/create.gif" />
                            新增离职手续办理单
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" ID="sz_shr_an" NavigateUrl="~/OM_Data/OM_LZSXBLD_SZ.aspx?action=read"
                            Target="_blank">
                            <asp:Image border="0" hspace="2" ImageAlign="AbsMiddle" Style="cursor: hand" runat="server"
                                ID="image2" ImageUrl="~/Assets/images/create.gif" />
                            审批人员设置
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblLZSXZT" AutoPostBack="true" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblLZSXZT_OnSelectedIndexChanged" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="2" Text="办理中"></asp:ListItem>
                            <asp:ListItem Value="3" Text="已通过"></asp:ListItem>
                            <asp:ListItem Value="4" Text="已驳回"></asp:ListItem>
                            <asp:ListItem Value="1" Selected="True" Text="我的审核任务"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        按部门：<asp:DropDownList runat="server" ID="ddlBM" AutoPostBack="true" OnSelectedIndexChanged="Query">
                        </asp:DropDownList>
                    </td>
                    <td>
                        按姓名：<asp:TextBox runat="server" ID="txtNAME"></asp:TextBox><asp:Button runat="server"
                            ID="btnQuery" Text="查询" OnClick="Query" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-outer">
        <div class="box-wrapper">
            <div id="div_statistcs" style="width: 100%; height: 450px; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater runat="server" ID="rptLZSQJSX" OnItemDataBound="rptLZSQJSX_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE; border-width: 1px; border-style: solid;
                                border-color: Black">
                                <td rowspan="2" style="border-width: 1px; border-style: solid; border-color: Black">
                                    序号
                                </td>
                                <td rowspan="2" style="border-width: 1px; border-style: solid; border-color: Black">
                                    姓名
                                </td>
                                <td rowspan="2" style="border-width: 1px; border-style: solid; border-color: Black">
                                    部门
                                </td>
                                <td colspan="5" style="border-width: 1px; border-style: solid; border-color: Black">
                                    手续办理状态
                                </td>
                                <td rowspan="2" style="border-width: 1px; border-style: solid; border-color: Black">
                                    确认离职时间
                                </td>
                                <td id="tdBBMSH" runat="server" rowspan="2" style="border-width: 1px; border-style: solid;
                                    border-color: Black">
                                    审核
                                </td>
                                <td id="tdAlter" runat="server" rowspan="2" style="border-width: 1px; border-style: solid;
                                    border-color: Black">
                                    修改
                                </td>
                                <td id="tdDelete" runat="server" rowspan="2" style="border-width: 1px; border-style: solid;
                                    border-color: Black">
                                    删除
                                </td>
                            </tr>
                            <tr align="center" style="background-color: #B9D3EE; border-width: 1px; border-style: solid;
                                border-color: Black">
                                <td style="border-width: 1px; border-style: solid; border-color: Black">
                                    本部门
                                </td>
                                <td style="border-width: 1px; border-style: solid; border-color: Black">
                                    其他部门
                                </td>
                                <td style="border-width: 1px; border-style: solid; border-color: Black">
                                    综合办公室
                                </td>
                                <td style="border-width: 1px; border-style: solid; border-color: Black">
                                    综合办公室部长
                                </td>
                                <td style="border-width: 1px; border-style: solid; border-color: Black">
                                    公司领导
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                ondblclick='trClick(<%#Eval("LZ_ID") %>)'>
                                <td>
                                    <asp:CheckBox runat="server" ID="cbxNumber" Visible="false" />
                                    <asp:Label runat="server" ID="lbNumber" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                    <asp:Label runat="server" ID="lbLZ_ID" Visible="false" Text='<%#Eval("LZ_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbName" Text='<%#Eval("LZ_PERSON") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbBuMen" Text='<%#Eval("LZ_BUMEN") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbLZ_BBMSHZT" Text='<%#Eval("LZ_BBMSHZT").ToString()=="0"?"办理中":Eval("LZ_BBMSHZT").ToString()=="y"?"已办理":"未办理"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbLZ_GBMSHZT" Text='<%#Eval("LZ_GBMSHZT").ToString()=="0"?"办理中":Eval("LZ_GBMSHZT").ToString()=="y"?"已办理":"未办理"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbLZ_ZHBSHZT" Text='<%#Eval("LZ_ZHBSHZT").ToString()=="0"?"办理中":Eval("LZ_ZHBSHZT").ToString()=="y"?"已办理":"未办理"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbLZ_ZHBSPZT" Text='<%#Eval("LZ_ZHBSPZT").ToString()=="y"?"已通过":Eval("LZ_ZHBSPZT").ToString()=="n"?"未通过":"未审批" %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="LZ_LDSPZT" Text='<%#Eval("LZ_LDSPZT").ToString()=="y"?"已通过":Eval("LZ_LDSPZT").ToString()=="n"?"未通过":"未审批"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="LZ_RIQI" Text='<%#Eval("LZ_RIQI") %>'></asp:Label>
                                </td>
                                <td id="tdBBMSH1" runat="server">
                                    <asp:HyperLink runat="server" ID="hplBBMSH" CssClass="link" NavigateUrl='<%#"OM_LZSXBLD.aspx?action=check&id="+Eval("LZ_ID")%>'>
                                        <asp:Image runat="server" ID="image4" Width="20px" Height="20px" border="0" hspace="2"
                                            align="absmiddle" ImageUrl="~/Assets/images/shenhe.gif" />
                                        审核
                                    </asp:HyperLink>
                                </td>
                                <td id="tdAlter1" runat="server">
                                    <asp:HyperLink runat="server" ID="hylAlter" CssClass="link" NavigateUrl='<%#"OM_LZSXBLD.aspx?action=alter&id="+Eval("LZ_ID")%>'>
                                        <asp:Image runat="server" ID="Image8" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                            ImageUrl="~/Assets/images/create.gif" />
                                        修改
                                    </asp:HyperLink>
                                </td>
                                <td id="tdDelete1" runat="server">
                                    <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除申请" OnClientClick="return confirm('确认删除这次申请？')"
                                        CommandArgument='<%#Eval("LZ_ID")%>' OnClick="lbtnDelete_OnClick">
                                        <asp:Image runat="server" ID="Image9" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                            ImageUrl="~/Assets/images/erase.gif" />
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <uc1:UCPaging runat="server" ID="UCPaging1" />
        </div>
    </div>
</asp:Content>
