<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="QC_Inspection_Detail.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Inspection_Detail" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div class="box-inner"><div class="box_right"><div class="box-title"><table width=100%><tr>
    <td>质量报检管理 <asp:Button ID="btn_export" runat="server" Text="导出"  OnClick="btn_export_Click"/></td></tr> </table></div></div></div>
   <div class="box-wrapper">      
       <div class="box-outer">
             <asp:Panel ID="ViewPanel" runat="server" width="80%">
               <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" frame="box">
                   <tr>
                       <td colspan="4" 
                           style="font-size: large; text-align: center; font-family: 黑体;">
                           报检单</td>
                   </tr>
                   <tr>
                      <td style="width: 20%" height="23px" >
                           报检部门：</td>
                       <td style="width: 30%" >
                          <asp:Label ID="lbdep" runat="server" Text=""></asp:Label></td>
                       <td style="width: 20%">
                            报检人：</td>
                       <td style="width: 30%" >
                          <asp:Label ID="lbmannm" runat="server" Text=""></asp:Label>
                          <asp:Label ID="lbid" runat="server" Text="" Visible="False"></asp:Label></td>
                    
                   </tr>
                   <tr>
                       <td height="23px">
                           项目名称：</td>
                       <td>
                           <asp:Label ID="lbpjname" runat="server" Text=""></asp:Label></td>
                       <td>
                           工程名称：</td>
                       <td>
                          <asp:Label ID="lbengname" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td height="23px">
                           部件名称：</td>
                       <td>
                          <asp:Label ID="lbpartname" runat="server" Text=""></asp:Label></td>
                       <td>
                           部件性质：</td>
                       <td>
                          <asp:Label ID="lbpartnature" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td height="23px">
                           图号/标识号：</td>
                       <td>
                          <asp:Label ID="lbtuhao" runat="server" Text=""></asp:Label></td>
                       <td>
                           数量：</td>
                       <td>
                          <asp:Label ID="lbquantity" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td height="23px">
                           控制内容：</td>
                       <td>
                            <asp:Label ID="lbcontroltxt" runat="server" Text=""></asp:Label></td>
                       <td>
                           资料搜集：</td>
                       <td>
                          <asp:Label ID="lbdataclct" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td height="23px">
                           报检日期：</td>
                       <td>
                          <asp:Label ID="lbpjdate" runat="server" Text=""></asp:Label></td>
                       <td>
                           需要检查日期：</td>
                       <td>
                           <asp:Label ID="lbrqstcdate" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td height="23px">
                           供货单位：</td>
                       <td>
                          <asp:Label ID="lbsupplyname" runat="server" Text=""></asp:Label></td>
                       <td>
                           检验地点：</td>
                       <td>
                           <asp:Label ID="lbsite" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                        <td height="23px">
                           联系人</td>
                       <td>
                          <asp:Label ID="lbcontact" runat="server" Text=""></asp:Label></td>
                       <td  >
                          电话：</td>
                       <td>
                           <asp:Label ID="lbtel" runat="server" Text=""></asp:Label>
                       </td>
                      
                   </tr>
                   <tr>
                       <td height="23px">
                           质检人：</td>
                           <td><asp:Label ID="lbqcmn" runat="server" Text="Label"></asp:Label></td>
                        <td>备注:</td>
                       <td>
                           <asp:Label ID="lbmeno" runat="server" Text=""></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td height="23px">
                           报检子项：</td>
                       <td colspan="3">
                           <asp:HiddenField ID="hfditem" runat="server" />
                           <asp:GridView ID="GridViewItem" runat="server" AutoGenerateColumns="False" 
                           OnRowDataBound="GridViewItem_RowDataBound" CellPadding="4" ForeColor="#333333">
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />            
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                        <asp:Label ID="lbid" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbagain" runat="server" Text='<%#Eval("ISAGAIN") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="23px" Width="30px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PARTNM" HeaderText="部件名称" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TUHAO" HeaderText="图号" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CAIZHI" HeaderText="材质" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DANZHONG" HeaderText="单重" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM" HeaderText="数量" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOTE" HeaderText="备注">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="是否已质检" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbisresult" runat="server" Text='<%#Eval("ISRESULT") .ToString()=="0"?"否":"是"%>' ></asp:Label>
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="质检结果" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbresult" runat="server" Text='<%#Eval("RESULT") %>'></asp:Label>
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                           </asp:GridView>
                       </td>
                   </tr>
               </table>
               <h3><font color="Red">
                   <asp:Label ID="LabelPD" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; </font></h3>
               <hr />
            <asp:Panel ID="QCResultPanel" runat="server">
               <table style="width: 100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" frame="box">
                   <tr>
                       <td colspan="4" style="font-size: large; text-align: center; font-family: 黑体;">
                           质检报告</td>
                   </tr>
                   <tr>
                       <td style="width: 15%" height="23px">
                           质检结果：</td>
                       <td style="width: 35%">
                           <asp:Label ID="lbrusult" runat="server" Text=""></asp:Label>
                           </td>
                       <td style="width: 15%">
                           检验日期：</td>
                       <td style="width: 35%">
                          <asp:Label ID="lbyjdate" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           检验报告：</td>
                       <td>
                       <div align="center" Width="98%">
                       <asp:Label ID="filesError" runat="server" EnableViewState="false" 
                            ForeColor="Red"></asp:Label>
                        <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                          <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5" 
                               CssClass="toptable grid"  CellPadding="4"   DataKeyNames="RP_ID" 
                               ForeColor="#333333" Width="98%">
                              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                              <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="RP_FILENAME" HeaderText="名称">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RP_UPLOADDATE" HeaderText="上传时间">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtndownload" runat="server"  ImageUrl="~/Assets/images/pdf.jpg" OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Height="18px" Width="18px" />   
                                    </ItemTemplate> 
                                    <ItemStyle  HorizontalAlign="Center"/> 
                                    <ControlStyle Font-Size="Small" /> 
                                     <HeaderStyle Width="30px" />
                                    </asp:TemplateField>
                                </Columns> 
                             <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small" Height="10px" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />  
                        </asp:GridView>
                        </div>
                        </td>
                       <td>
                           检验人员姓名：</td>
                       <td>
                           <asp:Label ID="lbinspectornm" runat="server" Text=""></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td >
                           检验说明：</td>
                       <td colspan="3" >
                           <asp:Label ID="lbinspctdscp" runat="server" Text=""></asp:Label>
                       </td>
                   </tr>
               </table>
              <%--  <asp:Panel ID="SubqualityPanel" runat="server" HorizontalAlign="Center" Visible="False">
                  <table style="width: 100%;" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                       <tr style="height: 25px">
                           <td style="font-size: large; text-align: center; font-family: 黑体;">
                               不合格说明事项</td>
                       </tr>
                       <tr>
                           <td style="text-align: right; width: 20%;">
                               </td>
                           <td style="text-align: right; width: 20%;">
                               不合格原因：</td>
                           <td style="text-align: left; width: 40%;">
                               <asp:Label ID="lbfailedrson" runat="server" Text=""></asp:Label>
                           </td>
                           <td style="text-align: left; width: 20%;">
                           </td>
                       </tr>
                       <tr>
                           <td style="text-align: right; width: 20%;">
                               </td>
                           <td style="text-align: right; width: 20%;">
                                解决办法：</td>
                           <td style="text-align: left; width: 40%;">
                               <asp:Label ID="lbsolution" runat="server" Text=""></asp:Label>
                           </td>
                           <td style="text-align: left; width: 20%;">
                           </td>
                       </tr>    
                   </table>
                </asp:Panel>--%>
           </asp:Panel>
               <h3  ><font color="Red">
               <asp:Label ID="lbguocheng" runat="server" Text="过程检验:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; </font></h3>
               <hr />
                <asp:Panel ID="plguocheng" runat="server" >
               <asp:GridView ID="GridView1"  CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  >
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
                    <asp:BoundField DataField="ISR_INSPCTORNM" HeaderText="质检人" 
                        ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="质检结果">
                        <ItemTemplate>
                            <asp:Label ID="lb_result" runat="server" Text='<%# Eval("ISR_RESULT").ToString()=="1"?"合格":"不合格" %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                  <asp:BoundField DataField="ISR_DATE" HeaderText="质检日期"  ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="质检报告">
                        <ItemTemplate>
                            <asp:Panel ID="Panel_Add_Report" runat="server">
                            <asp:Label ID="lb_report" runat="server" Text='<%# Bind("ISR_REPORT") %>' Visible="false"></asp:Label>
                            </asp:Panel>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ISR_INSPCTDSCP" HeaderText="检验说明" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ISR_NOTE" HeaderText="备注" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small" Height="10px" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />                    
            </asp:GridView>   
           </asp:Panel>
           <h2 align="center">
           <asp:Button ID="back" runat="server" Text=" 返  回 " onclick="back_Click" />
           </h2>    
          </asp:Panel>
           </div>
         </div>
</asp:Content>
