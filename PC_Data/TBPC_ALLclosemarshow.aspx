<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBPC_ALLclosemarshow.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.TBPC_ALLclosemarshow" MasterPageFile="~/Masters/RightCotentMaster.master" Title="全部行关闭物料查看"%>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    全部行关闭物料查看</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
   </cc1:ToolkitScriptManager>
       <asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>
  <script language="JavaScript" type="text/javascript">    
    function openbom()
    {
      window.open("../TM_Data/TM_Design_Bom.aspx");
    }
  </script>   
     <div class="box-inner" style="vertical-align:top">
            <div class="box_right">
                <div class="box-title">
                    <table width="98%">
                        <tr>
                        <td  align="center">
                         <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif"  border="0" hspace="2" 
                                        align="absmiddle" runat="server" />筛选</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-450"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </asp:PopupControlExtender>                             
                        </td>
                        </tr>
                    </table>
                    <asp:Panel ID="palORG" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                             <table width="80%" >
                              <tr>       
                                 <td>
                                      <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                          <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                      </div>
                                      <br /><br />
                                 </td>
                              </tr>
                                <tr>
                                  <td>
                                    批号：
                                  </td>
                                   <td>
                                       <asp:TextBox ID="tb_pcode" runat="server"></asp:TextBox>
                                  </td>
                                   <td>
                                    计划号：
                                  </td>
                                   <td>
                                       <asp:TextBox ID="tb_ptcode" runat="server"></asp:TextBox>
                                  </td>
                                  <td>
                                    项目名称：
                                  </td>
                                   <td>
                                       <asp:TextBox ID="tb_pjnm" runat="server"></asp:TextBox>
                                  </td>
                                   <td>
                                    工程名称：
                                  </td>
                                   <td>
                                       <asp:TextBox ID="tb_engnm" runat="server"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                   <td>
                                   名称：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_marnm" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                   规格：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_margg" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                   材质：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_marcz" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                   国标：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_margb" runat="server"></asp:TextBox>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                    关闭人：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="8" align="center">
                                   <asp:Button ID="btnQuery"  runat="server" Text="查询" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                                   <asp:Button ID="btnReset" runat="server" OnClick="btnReset_OnClick" UseSubmitBehavior="false" Text="重置" />
                                </td>
                                </tr>
                             </table>
                            </asp:Panel>
                </div>
            </div>
    </div>
    
     <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="NoDataPanel" Width="100%"  runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel> 
             <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="PUR_PCODE" runat="server" Text='<%# Eval("planno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="计划号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="PUR_PTCODE" runat="server" Text='<%# Eval("ptcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="PUR_NOTE" runat="server" Text='<%# Eval("PUR_ZYDY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
 
                <asp:BoundField DataField="pjnm" HeaderText="项目" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="engnm" HeaderText="工程" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="marid" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="marnm" HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="margg" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="marcz" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="margb" HeaderText="国标" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="PUR_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="length" HeaderText="长度" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="width" HeaderText="宽度" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:TemplateField HeaderText="关闭人" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="cgrnm" runat="server" Text='<%# Eval("cgrnm1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="cgrnm" HeaderText="采购人" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />--%>
                
                <asp:BoundField DataField="num" HeaderText="需用数量"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="marunit" HeaderText="单位"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                 <asp:BoundField DataField="fznum" HeaderText="需用辅助数量"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                  <asp:BoundField DataField="marfzunit" HeaderText="辅助单位"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
               <%--<asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <ItemTemplate>        
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../Assets/images/no.gif" title="删除"  OnCommand="delete_Click"  
                         CommandName='<%#Eval("KC_ID")%>' OnClientClick="javascript:return confirm('确认删除吗？')"/>
                     </ItemTemplate>
               </asp:TemplateField>--%>
            </Columns>
            <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle Wrap="false" BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="440px" TableWidth="100%" FixColumns="0" />     
        </yyc:SmartGridView>
            
            <uc1:UCPaging ID="UCPaging1" Visible="false" runat="server" />
        </div>
    </div>
    
  </asp:Content>
    
