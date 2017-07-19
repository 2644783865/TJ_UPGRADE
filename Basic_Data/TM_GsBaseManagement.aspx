<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_GsBaseManagement.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.TM_GsBaseManagement" Title="工时基础数据管理" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工时基础数据
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script type="text/javascript">
         
    </script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
        <div class="box-wrapper">
            <div class="box-outer">
                <table>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            <asp:RadioButton ID="radio_zaiyong" runat="server" Text="在用" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" Checked="true" />
                            <asp:RadioButton ID="radio_tingyong" runat="server" Text="停用" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;
                            产品名称:<input id="iptcpname" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;
                            产品规格:<input id="iptcpguige" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;
                            总图号:<input id="iptzongmap" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;
                            部件名称:<input id="iptbjname" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;
                            部件图号:<input id="iptbjmap" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;
                            <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
                         </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Basic_Data/TM_GsBaseDetail.aspx?action=add" Target="_blank" runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <%--<tr>
                        
                        <td>
                            
                        </td>
                        <td>
                            
                        </td>
                    </tr>--%>
                </table>
            </div>
        </div>
        <div class="box-wrapper">
                <div class="box-outer">
                    <div style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptgsbasemanagement" runat="server" OnItemDataBound="rptgsbasemanagement_itemdatabind">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            编号
                                        </td>
                                        <td align="center">
                                            产品名称
                                        </td>
                                        <td align="center">
                                            产品规格
                                        </td>
                                        <td align="center">
                                            总图号
                                        </td>
                                        <td align="center">
                                            部件图号
                                        </td>
                                        <td align="center">
                                            部件名称
                                        </td>
                                        <td align="center">
                                            单件工时
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            使用状态
                                        </td>
                                        <td>
                                            制定人
                                        </td>
                                        <td>
                                            制定时间
                                        </td>
                                        <td align="center">
                                            查看
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td align="center">
                                            删除
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                        <td align="center">
                                            <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="context" runat="server" Text='<%#Eval("context")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="cpname" runat="server" Text='<%#Eval("cpname")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="cpguige" runat="server" Text='<%#Eval("cpguige")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="zongmap" runat="server" Text='<%#Eval("zongmap")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="bjmap" runat="server" Text='<%#Eval("bjmap")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="bjname" runat="server" Text='<%#Eval("bjname")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="bjpergs" runat="server" Text='<%#Eval("bjpergs")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:TextBox ID="notetotal" TextMode="MultiLine" ToolTip='<%#Eval("notetotal")%>' Width="80px" Height="32px" runat="server" Text='<%#Eval("notetotal")%>'></asp:TextBox>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="state" runat="server" Text='<%#Eval("state").ToString()=="0"?"在用":"停用"%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="zdrname" runat="server" Text='<%#Eval("zdrname")%>'></asp:Label>
                                            <asp:Label ID="zdrid" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="zdtime" runat="server" Text='<%#Eval("zdtime")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%#"~/Basic_Data/TM_GsBaseDetail.aspx?action=view&context="+Eval("context")%>' Target="_blank" runat="server">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />查看</asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%#"~/Basic_Data/TM_GsBaseDetail.aspx?action=edit&context="+Eval("context")%>' Target="_blank" runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:LinkButton ID="LinkButtonSC" OnClick="btndelete_OnClick" CommandArgument='<%# Eval("context")%>'  OnClientClick="return confirm('将删除该部件图号下的所有内容，确认删除吗?')" runat="server">
                                                <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />删除</asp:LinkButton>
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
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
