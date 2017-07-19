<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_JICHUANGBASE.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.TM_JICHUANGBASE" Title="机床基础数据" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
机床基础数据
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
            <div class="box-outer">
                <table>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            <asp:RadioButton ID="radio_zaiyong" runat="server" Text="在用" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" Checked="true" />
                            <asp:RadioButton ID="radio_tingyong" runat="server" Text="停用" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            机床编号:<input id="iptjcbh" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;&nbsp;
                            机床类型:<input id="iptjctype" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;&nbsp;
                            适用工序:<input id="iptjcable" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;&nbsp;
                            添加人:<input id="iptaddman" class="easyui-textbox" runat="server" style="width:80px" />&nbsp;&nbsp;&nbsp;
                            <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Basic_Data/TM_JICHUANGBASEDETAIL.aspx?action=add" Target="_blank" runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="box-wrapper">
                <div class="box-outer">
                    <div style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptjclook" runat="server" OnItemDataBound="rptjclook_itemdatabind">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            机床编号
                                        </td>
                                        <td align="center">
                                            机床类型
                                        </td>
                                        <td align="center">
                                            适用工序
                                        </td>
                                        <td align="center">
                                            人员配置
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            状态
                                        </td>
                                        <td align="center">
                                            添加人
                                        </td>
                                        <td align="center">
                                            添加时间
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
                                            <asp:Label ID="id" runat="server" Text='<%#Eval("id")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_bh" runat="server" Text='<%#Eval("jc_bh")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_type" runat="server" Text='<%#Eval("jc_type")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_gxtypeable" runat="server" Text='<%#Eval("jc_gxtypeable")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_containper" runat="server" Text='<%#Eval("jc_containper")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                             <asp:TextBox ID="jc_note" TextMode="MultiLine" ToolTip='<%#Eval("jc_note")%>' Width="80px" Height="32px" runat="server" Text='<%#Eval("jc_note")%>'></asp:TextBox>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_state" runat="server" Text='<%#Eval("jc_state").ToString().Trim()=="0"?"在用":"停用"%>'></asp:Label>
                                        </td>
                                        
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_addman" runat="server" Text='<%#Eval("jc_addman")%>'></asp:Label>
                                            <asp:Label ID="jc_addmanid" runat="server" Visible="false" Text='<%#Eval("jc_addmanid")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="jc_addtime" runat="server" Text='<%#Eval("jc_addtime")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:LinkButton ID="LinkButtonSC" OnClick="btndelete_OnClick" CommandArgument='<%# Eval("id")%>'  OnClientClick="return confirm('确认删除吗?')" runat="server">
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
