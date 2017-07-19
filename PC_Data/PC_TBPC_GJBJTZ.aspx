<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_GJBJTZ.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_GJBJTZ" MasterPageFile="~/Masters/RightCotentMaster.master" Title="关键部件台账"%>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    关键部件台账</asp:Content>
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
                        <td align="right" width="50%">
                         <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif"  border="0" hspace="2" 
                                        align="absmiddle" runat="server" />筛选</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </asp:PopupControlExtender>
                             
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Bom" runat="server" Text="到Bom原始数据"  OnClientClick="openbom()" />
                        </td>
                        </tr>
                    </table>
                    <asp:Panel ID="palORG" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                             <table width="40%" >
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
                                   构件名称：
                                   </td>
                                   <td colspan="3">
                                       <asp:TextBox ID="tb_gjnm" runat="server"></asp:TextBox>
                                   </td>
                                </tr>
                                <tr>
                                <td colspan="4" align="center">
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
                        <%--<asp:CheckBox ID="CheckBox1" runat="server"  CssClass="checkBoxCss" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("KC_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="PJ_NAME" runat="server" Text='<%# Eval("PJ_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="工程名称" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%# Eval("TSA_ENGNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
 
                <asp:BoundField DataField="KC_GJNM" HeaderText="构件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_LPH" HeaderText="炉批号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_CGJCSJ" HeaderText="进厂时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_CGJCZT" HeaderText="进厂状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_CYJSTM" HeaderText="接收时间" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="KC_ZLCSJC" HeaderText="超声波检测" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_ZLCFJC" HeaderText="磁粉检测" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_ZLCCJC" HeaderText="尺寸检查" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_CYRKTM" HeaderText="入库时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_CYCKTM" HeaderText="出库时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="KC_SCTM" HeaderText="喷砂完成时间"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
               <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract" CssClass="link" 
                         NavigateUrl='<%#"~/PC_Data/PC_TBPC_GJBJTZDetail.aspx?Action=Edit&kc_id="+Eval("KC_ID")%>' runat="server">
                        <asp:Image ID="Image_edit" ImageUrl="~/Assets/images/modify.gif"  runat="server"  ToolTip="修改"/>                               
                        </asp:HyperLink>
                    </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <ItemTemplate> 
                         <asp:HyperLink ID="HyperLink2" CssClass="link" 
                         NavigateUrl='<%#"~/PC_Data/PC_TBPC_GJBJTZDetail.aspx?Action=Look&kc_id="+Eval("KC_ID")%>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/search.gif"   runat="server" ToolTip="查看"/>                               
                        </asp:HyperLink>
                  </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <ItemTemplate>        
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../Assets/images/no.gif" title="删除"  OnCommand="delete_Click"  
                         CommandName='<%#Eval("KC_ID")%>' OnClientClick="javascript:return confirm('确认删除吗？')"/>
                     </ItemTemplate>
               </asp:TemplateField>
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
    