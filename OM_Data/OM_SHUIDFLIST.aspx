<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_SHUIDFLIST.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHUIDFLIST"
    Title="住宿水电费管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿水电费管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp; 楼层：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                            <asp:ListItem Value="1">一楼</asp:ListItem>
                            <asp:ListItem Value="2">二楼</asp:ListItem>
                            <asp:ListItem Value="3">三楼</asp:ListItem>
                            <asp:ListItem Value="4">四楼</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp; 房间号：<asp:TextBox ID="tbfjnum" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp; 姓名：<asp:TextBox ID="tbname" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                    </td>
                    <td align="right">
                        <a href="OM_SHUIDFSP_detail.aspx?action=add&spbh=''" target="_blank" style="color: #ff6600;
                            width: 70px;" runat="server" class="easyui-linkbutton">提交审批</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnsave" runat="server" Text="保存" OnClick="btnsave_click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnfjh" runat="server" Text="删除" OnClientClick="javascript:return confirm('是否确定删除?')" OnClick="btnfjh_click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplshuidf" runat="server" NavigateUrl="~/OM_Data/OM_shuidfdetail.aspx?action=add"
                            Target="_blank" Font-Underline="false">
                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                ImageAlign="AbsMiddle" runat="server" Width="20px" />新增水电费数据</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hpleditprice" runat="server" NavigateUrl="~/OM_Data/OM_sdpriceedit.aspx?"
                            Target="_blank" Font-Underline="false">
                            <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                ImageAlign="AbsMiddle" runat="server" Width="20px" />修改水电费价格</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplsimplequery" runat="server" NavigateUrl="~/OM_Data/OM_SHUIDFSQ.aspx?"
                            Target="_blank" Font-Underline="false">
                            <asp:Image ID="Image3" ImageUrl="~/Assets/images/search.gif" border="0" hspace="2"
                                ImageAlign="AbsMiddle" runat="server" Width="20px" />水电费简易查询</asp:HyperLink>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptsushe" runat="server" OnItemDataBound="rpt_OnItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE;" height="30px">
                                <td align="center">
                                    序号
                                </td>
                                <td align="center">
                                    宿舍号
                                </td>
                                <td align="center">
                                    起始日期
                                </td>
                                <td align="center">
                                    截止日期
                                </td>
                                <td align="center">
                                    起始电表读数
                                </td>
                                <td align="center">
                                    截止电表读数
                                </td>
                                <td align="center">
                                    公司承担度数
                                </td>
                                <td align="center">
                                    个人承担度数
                                </td>
                                <td align="center">
                                    用电单价
                                </td>
                                <td align="center">
                                    需承担电费金额
                                </td>
                                <td align="center">
                                    起始水表读数
                                </td>
                                <td align="center">
                                    截止水表读数
                                </td>
                                <td align="center">
                                    公司承担吨数
                                </td>
                                <td align="center">
                                    个人承担吨数
                                </td>
                                <td align="center">
                                    用水单价
                                </td>
                                <td align="center">
                                    需承担水费金额
                                </td>
                                <td align="center">
                                    姓名
                                </td>
                                <td align="center">
                                    人均费用
                                </td>
                                <td align="center">
                                    实际费用
                                </td>
                                <td align="center">
                                    修改
                                </td>
                                <td align="center">
                                    备注
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" height="30px">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <asp:Label ID="IDsdmx" runat="server" Text='<%#Eval("IDsdmx")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                    <asp:Label ID="IDSDF" runat="server" Text='<%#Eval("IDSDF")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="ssnum" runat="server" Text='<%#Eval("ssnum")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="startdate" runat="server" Text='<%#Eval("startdate")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="enddate" runat="server" Text='<%#Eval("enddate")%>' Visible="false"></asp:Label>
                                </td>
                                <td id="td_IDSDF" runat="server" visible="false">
                                    <%#Eval("IDSDF")%>'
                                </td>
                                <td id="td_ssnum" runat="server" align="center">
                                    <%#Eval("ssnum")%>
                                </td>
                                <td id="td_startdate" runat="server" align="center">
                                    <%#Eval("startdate")%>
                                </td>
                                <td id="td_enddate" runat="server" align="center">
                                    <%#Eval("enddate")%>
                                </td>
                                <td id="td_stratdf" runat="server" align="center">
                                    <%#Eval("stratdf")%>
                                </td>
                                <td id="td_enddf" runat="server" align="center">
                                    <%#Eval("enddf")%>
                                </td>
                                <td id="td_gscddl" runat="server" align="center">
                                    <%#Eval("gscddl")%>
                                </td>
                                <td id="td_shiydl" runat="server" align="center">
                                    <%#Eval("shiydl")%>
                                </td>
                                <td id="td_pricedf" runat="server" align="center">
                                    <%#Eval("pricedf")%>
                                </td>
                                <td id="td_dianfje" runat="server" align="center">
                                    <%#Eval("dianfje")%>
                                </td>
                                <td id="td_startsf" runat="server" align="center">
                                    <%#Eval("startsf")%>
                                </td>
                                <td id="td_endsf" runat="server" align="center">
                                    <%#Eval("endsf")%>
                                </td>
                                <td id="td_gscdsl" runat="server" align="center">
                                    <%#Eval("gscdsl")%>
                                </td>
                                <td id="td_shiysl" runat="server" align="center">
                                    <%#Eval("shiysl")%>
                                </td>
                                <td id="td_pricesf" runat="server" align="center">
                                    <%#Eval("pricesf")%>
                                </td>
                                <td id="td_shuifje" runat="server" align="center">
                                    <%#Eval("shuifje")%>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                </td>
                                <td id="td_renjunfy" runat="server" align="center">
                                    <%#Eval("renjunfy")%>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_realmoney" runat="server" Text='<%#Eval("realmoney")%>' Width="60px"></asp:TextBox>
                                </td>
                                <td id="td_edit" runat="server" align="center">
                                    <asp:HyperLink ID="hledit" Target="_blank" ToolTip="修改" NavigateUrl='<%#"~/OM_Data/OM_shuidfdetail.aspx?action=edit&id="+Eval("IDSDF") %>'
                                        CssClass="link" runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </td>
                                <td id="td_note" runat="server" align="center">
                                    <%#Eval("note")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                    <tr>
                                        <td colspan="6">
                                            合计：
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_gsdlhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_grdlhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_hjdf" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_gsslhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_grslhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_hjsf" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_rjhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_realhj" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            
                                        </td>
                                    </tr>
                                </FooterTemplate>
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
