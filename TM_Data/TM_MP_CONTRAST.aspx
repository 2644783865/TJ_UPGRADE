<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_MP_CONTRAST.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_CONTRAST" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
���ϼƻ�����Աȱ�
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
                            ����ţ�
                            <asp:Label ID="tsa_id" runat="server"></asp:Label>
                        </td>
                        <td>
                            ��ͬ�ţ�
                            <asp:Label ID="lab_contract" runat="server"></asp:Label>
                            <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                            <input id="mp_no" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                            <input id="status" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                                                <td>
                            ��Ŀ���ƣ�
                            <asp:Label ID="lab_proname" runat="server"></asp:Label>
                        </td>
                        <td>
                            �豸���ƣ�
                            <asp:Label ID="lab_engname" runat="server"></asp:Label>
                        </td>
                       
                        <td align="right">
                        <asp:Button ID="btnComplete" runat="server" Text="����ύ" OnClientClick="return confirm(&quot;ȷ�ϸ�������ƻ�������ύ��&quot;);" OnClick="btnComplete_Click" />
                        <asp:Button ID="btnreturn" runat="server" Text="�� ��"  />
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
                        ��������:<asp:DropDownList ID="ddlXuhao" runat="server" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                      <%-- <td align="left">
                        ���Ϲ��:<asp:DropDownList ID="ddlMarGuiGe" runat="server"  OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                           </asp:DropDownList>
                        </td>                            
                       <td align="left">
                        ����:<asp:DropDownList ID="ddlMarCaiZhi" runat="server"  OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged" AutoPostBack="true">
                           </asp:DropDownList>
                        </td>  --%>
                       
                         
                        <td align="left">
                            �������:
                            <asp:DropDownList ID="ddlmpName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged">
                                <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                <asp:ListItem Text="�ֲ�" Value="1"></asp:ListItem>
                                <asp:ListItem Text="��׼��" Value="2"></asp:ListItem>
                                <asp:ListItem Text="���ͼ�" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="�ǽ���" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="�ɹ���Ʒ" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">��ϸ/����:
                            <asp:RadioButton ID="rad_detail" runat="server" Text="��ϸ��Ϣ" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_detail_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                            <asp:RadioButton ID="rad_sum" runat="server" Text="������Ϣ" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_sum_CheckedChanged" AutoPostBack="true" />&nbsp;
                        </td>
                        <td align="left">
                        </td>
                        <td></td>
                        <td></td>
                         </tr>
                        
                        </table>
                         <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                            <hr style="width:100%; height:0.1px; color:Blue;" />û�м�¼������</asp:Panel>
                            
                         <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                                 CellPadding="4" ForeColor="#333333" Width="100%">
                            <RowStyle BackColor="#EFF3FB"/>
                            <Columns>
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="����" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="ͼ��" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MARID" HeaderText="���ϱ���" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="��������" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                  <asp:TemplateField HeaderText="��������" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="���" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="����(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="���(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="��λ" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="��������" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="BM_MPMY" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="����(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="�����ܳ�(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="�Ƿ񶨳�"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="��ע" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />

                               
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
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="����" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="ͼ��" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MARID" HeaderText="���ϱ���" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="��������" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                  <asp:TemplateField HeaderText="��������" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="���" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="����(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="���(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="��λ" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                   <asp:BoundField DataField="BM_YONGLIANG" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="BM_MPMY" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="����(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="�����ܳ�(kg)"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="����" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="�Ƿ񶨳�"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("BM_FIXEDSIZE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="��ע" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />

                               
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
                                <asp:TemplateField HeaderText="�к�" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ͼ��" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���ϱ���" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��������" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���" DataField="BM_MAGUIGE" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����" DataField="BM_MAQUALITY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����(mm)" DataField="BM_MALENGTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���(mm)" DataField="BM_MAWIDTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��λ" DataField="BM_TECHUNIT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="��������" DataField="BM_YONGLIANG" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����" DataField="BM_MATOTALWGHT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                              
                                <asp:BoundField HeaderText="����(�O)" DataField="BM_MPMY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��׼" DataField="BM_STANDARD" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderText="��������"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�Ƿ񶨳�"  HeaderStyle-Wrap="false">
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
                                <asp:TemplateField HeaderText="�к�" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ͼ��" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���ϱ���" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��������" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���" DataField="BM_MAGUIGE" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����" DataField="BM_MAQUALITY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����(mm)" DataField="BM_MALENGTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="���(mm)" DataField="BM_MAWIDTH" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��λ" DataField="BM_TECHUNIT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField HeaderText="��������" DataField="BM_YONGLIANG" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="����" DataField="BM_MATOTALWGHT" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                              
                                <asp:BoundField HeaderText="����(�O)" DataField="BM_MPMY" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="��׼" DataField="BM_STANDARD" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField ItemStyle-Wrap="false" HeaderText="��������"  HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("BM_MASHAPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�Ƿ񶨳�"  HeaderStyle-Wrap="false">
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
       <td align="left" style="background-color:Yellow; font-size:medium;">���ݴ����У����Ժ�...</td>
       </tr>
       </table>
       </div>
                    
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
