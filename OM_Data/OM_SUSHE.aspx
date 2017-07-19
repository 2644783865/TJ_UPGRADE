<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_SUSHE.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SUSHE" Title="宿舍管理" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  宿舍管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
                        &nbsp;&nbsp;&nbsp;
                        楼层：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="1">一楼</asp:ListItem>
                                            <asp:ListItem Value="2">二楼</asp:ListItem>
                                            <asp:ListItem Value="3">三楼</asp:ListItem>
                                            <asp:ListItem Value="4">四楼</asp:ListItem>
                                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        房间号：<asp:TextBox ID="tbfjnum" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        姓名：<asp:TextBox ID="tbname" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                   </td>
                   <td align="right">
                        <asp:Button ID="btnfjh" runat="server" Text="删除房间号" OnClick="btnfjh_click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplsushe" runat="server" NavigateUrl="~/OM_Data/OM_SUSHEDETAIL.aspx?action=add" Target="_blank" Font-Underline="false"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" Width="20px" />新增宿舍信息</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                      <asp:Repeater ID="rptsushe" runat="server">
                          <HeaderTemplate>
                             <tr style="background-color: #B9D3EE;" height="30px">
                                <td align="center">
                                序号
                                </td>
                                <td align="center">
                                房间号
                                </td>
                                <td align="center">
                                现有人数
                                </td>
                                <td align="center">
                                容积人数
                                </td>
                                <td align="center">
                                可入住数量
                                </td>
                                <td align="center">
                                姓名
                                </td>
                                <td align="center">
                                部门
                                </td>
                                <td align="center">
                                上下铺
                                </td>
                                <td align="center">
                                组合床
                                </td>
                                <td align="center">
                                单人床
                                </td>
                                <td align="center">
                                衣柜数量
                                </td>
                                <td align="center">
                                椅子数量
                                </td>
                                <td align="center">
                                电视
                                </td>
                                <td align="center">
                                电视柜
                                </td>
                                <td align="center">
                                空调
                                </td>
                                <td align="center">
                                写字台
                                </td>
                                <td align="center">
                                可调床铺数
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
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                    <asp:Label ID="ID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="housenum" runat="server" Text='<%#Eval("housenum")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                </td>
                                <td id="td_housenum" runat="server" align="center">
                                    <%#Eval("housenum")%>
                                </td>
                                <td id="td_xyrs" runat="server" align="center">
                                    <%#Eval("xyrs")%>
                                </td>
                                <td id="td_rjrs" runat="server" align="center">
                                    <%#Eval("rjrs")%>
                                </td>
                                <td id="td_krzsl" runat="server" align="center">
                                    <%#Eval("krzsl")%>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="DEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td id="td_shangxp" runat="server" align="center">
                                    <%#Eval("shangxp").ToString()=="0"?"":Eval("shangxp")%>
                                </td>
                                
                                <td id="td_zuhc" runat="server" align="center">
                                    <%#Eval("zuhc").ToString() == "0" ? "" : Eval("zuhc")%>
                                </td>
                                <td id="td_danrc" runat="server" align="center">
                                    <%#Eval("danrc").ToString() == "0" ? "" : Eval("danrc")%>
                                </td>
                                <td id="td_yignum" runat="server" align="center">
                                    <%#Eval("yignum").ToString() == "0" ? "" : Eval("yignum")%>
                                </td>
                                
                                <td id="td_yiznum" runat="server" align="center">
                                    <%#Eval("yiznum").ToString() == "0" ? "" : Eval("yiznum")%>
                                </td>
                                <td id="td_diansbum" runat="server" align="center">
                                    <%#Eval("diansbum").ToString() == "0" ? "" : Eval("diansbum")%>
                                </td>
                                <td id="td_diansgnum" runat="server" align="center">
                                    <%#Eval("diansgnum").ToString() == "0" ? "" : Eval("diansgnum")%>
                                </td>
                                <td id="td_kongtnum" runat="server" align="center">
                                    <%#Eval("kongtnum").ToString() == "0" ? "" : Eval("kongtnum")%>
                                </td>
                                <td id="td_xieztnum" runat="server" align="center">
                                    <%#Eval("xieztnum").ToString() == "0" ? "" : Eval("xieztnum")%>
                                </td>
                                <td id="td_ketcpnum" runat="server" align="center">
                                    <%#Eval("ketcpnum").ToString() == "0" ? "" : Eval("ketcpnum")%>
                                </td>
                                <td id="td_edit" runat="server" align="center">
                                <asp:HyperLink ID="hledit" Target="_blank" ToolTip="修改" NavigateUrl='<%#"~/OM_Data/OM_SUSHEDETAIL.aspx?action=edit&id="+Eval("housenum") %>'
                            CssClass="link" runat="server">
                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                            修改
                        </asp:HyperLink>
                                </td>
                                <td id="td_notess" runat="server" align="center">
                                    <%#Eval("notess")%>
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
