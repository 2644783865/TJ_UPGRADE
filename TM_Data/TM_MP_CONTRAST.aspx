<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_MP_CONTRAST.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_CONTRAST" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
材料计划变更对比表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td>
                            任务号：
                            <asp:Label ID="tsa_id" runat="server"></asp:Label>
                        </td>
                        <td>
                            合同号：
                            <asp:Label ID="lab_contract" runat="server"></asp:Label>
                            <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                            <input id="mp_no" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                            <input id="status" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                                                <td>
                            项目名称：
                            <asp:Label ID="lab_proname" runat="server"></asp:Label>
                        </td>
                        <td>
                            设备名称：
                            <asp:Label ID="lab_engname" runat="server"></asp:Label>
                        </td>
                       
                        <td align="right">
                        <asp:Button ID="btnComplete" runat="server" Text="完成提交" OnClientClick="return confirm(&quot;确认该批变更计划已完成提交吗？&quot;);" OnClick="btnComplete_Click" />
                        <asp:Button ID="btnreturn" runat="server" Text="返 回"  />
                            &nbsp;&nbsp;
                           </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                      <table width="100%">
                         <tr>
                          
                        <td align="left">
                        部件名称:<asp:DropDownList ID="ddlXuhao" runat="server" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                      <%-- <td align="left">
                        材料规格:<asp:DropDownList ID="ddlMarGuiGe" runat="server"  OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                           </asp:DropDownList>
                        </td>                            
                       <td align="left">
                        材质:<asp:DropDownList ID="ddlMarCaiZhi" runat="server"  OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                           </asp:DropDownList>
                        </td>  --%>
                       
                         
                        <td align="left">
                            材料类别:
                            <asp:DropDownList ID="ddlmpName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="钢材" Value="1"></asp:ListItem>
                                <asp:ListItem Text="标准件" Value="2"></asp:ListItem>
                                <asp:ListItem Text="铸锻件" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="非金属" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="采购成品" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">详细/汇总:
                            <asp:RadioButton ID="rad_detail" runat="server" Text="详细信息" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_detail_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                            <asp:RadioButton ID="rad_sum" runat="server" Text="汇总信息" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_sum_CheckedChanged" AutoPostBack="true" />&nbsp;
                        </td>
                        <td align="left">
                        </td>
                        <td></td>
                        <td></td>
                         </tr>
                        
                        </table>
                         <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                            <hr style="width:100%; height:0.1px; color:Blue;" />没有记录！！！</asp:Panel>
                            
                         <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                                 CellPadding="4" ForeColor="#333333" Width="100%">
                            <RowStyle BackColor="#EFF3FB"/>
                            <Columns>
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="材料名称" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                  <asp:TemplateField HeaderText="材料种类" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="规格" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="BM_MPMY" HeaderText="面域" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="重量(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="材料总长(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="数量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />

                               
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>
                    
                    
                    
                    
                     <yyc:SmartGridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                                 CellPadding="4" ForeColor="#333333" Width="100%">
                            <RowStyle BackColor="#EFF3FB"/>
                            <Columns>
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="材料名称" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                  <asp:TemplateField HeaderText="材料种类" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="规格" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                   <asp:BoundField DataField="BM_YONGLIANG" HeaderText="用量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="BM_MPMY" HeaderText="面域" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="重量(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="材料总长(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="数量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />

                               
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>
                    
                    
                        
                         <yyc:SmartGridView ID="GridView3" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                            Visible="false">
                            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="图号" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="材料名称" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="规格" DataField="BM_MAGUIGE" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="长度(mm)" DataField="BM_MALENGTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="宽度(mm)" DataField="BM_MAWIDTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="单位" DataField="BM_TECHUNIT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="材料用量" DataField="BM_YONGLIANG" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="重量" DataField="BM_MATOTALWGHT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                              
                                <asp:BoundField HeaderText="面域(㎡)" DataField="BM_MPMY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="标准" DataField="BM_STANDARD" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderText="材料种类"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc2" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                                                                           
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle Wrap="false" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView> 
                                             <yyc:SmartGridView ID="GridView4" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                            Visible="false">
                            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="图号" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="材料名称" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="规格" DataField="BM_MAGUIGE" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="长度(mm)" DataField="BM_MALENGTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="宽度(mm)" DataField="BM_MAWIDTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="单位" DataField="BM_TECHUNIT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField HeaderText="材料用量" DataField="BM_YONGLIANG" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="重量" DataField="BM_MATOTALWGHT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                              
                                <asp:BoundField HeaderText="面域(㎡)" DataField="BM_MPMY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="标准" DataField="BM_STANDARD" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderText="材料种类"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc2" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                                                                           
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle Wrap="false" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>                  
                         <uc1:UCPaging ID="UCPagingMS" runat="server" />
                   </div>
                 </div>
                 </ContentTemplate>
            </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 70%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
                    
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
