<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="CM_task_detail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_task_detail" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务单管理       
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
 
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    
          <script type="text/javascript" language="javascript">   
          
      /*检验输入数量并统计*/
        function checkEN(tb)
        {
            Statistic();
         }
          function Statistic() {
            var tn = 0;
            var gv1 = document.getElementById("gr");
            for (i = 1; i < (gv1.rows.length - 1); i++)
            {
                var val1 = gv1.rows[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
                if(isNaN(parseFloat(val1)))
                {
                  continue;
                }
                tn += parseFloat(val1);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0];
            lbtn.innerHTML = tn;
        }
       </script>
     
     
        <div class="box-outer">          


            <asp:HiddenField ID="HiddenFieldContent" runat="server" />
            <asp:HiddenField ID="HiddenFieldContent1" runat="server" />
            <table width="100%">
              <tr>
                    <td colspan="3" align="right">
                        <asp:Button ID="btnsubmit" runat="server" Text="确定"  onclick="btnsubmit_Click"/>
                        <asp:Button ID="btnjs" runat="server" Text="结算"  onclick="btnjs_Click"/>
                         &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnreturn" runat="server" Text="返 回" 
                            onclick="btnreturn_Click" CausesValidation="False" />
                         &nbsp;&nbsp;
                    </td>
                </tr>
          </table>
          
          <asp:Panel ID="pl_js" runat="server" >
            <table width="100%" >
                <tr align="left">
                    <td  align="right" width="15%">负责部门:</td>
                   <td width="15%">
                       <asp:Label ID="lb_dep" runat="server" Text=""></asp:Label></td>
                    <td  align="right" width="15%">发包金额:</td>
                    <td width="15%" >
                        <asp:TextBox ID="txt_je" runat="server"></asp:TextBox>
                      </td>
                      <td  align="right" width="15%">是否完成结算:</td>
                    <td  >
                        <asp:DropDownList ID="ddl_isjs" runat="server">
                         <asp:ListItem Value="">-请选择-</asp:ListItem>        
                         <asp:ListItem Value="0">否</asp:ListItem>
                         <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                       
                </tr>
            </table>
             <hr />
            </asp:Panel>             
  
        <asp:Panel ID="panel3" runat="server" >       
        <table width="100%">
            <tr align="center">
                <td style=" font-size:large; text-align:center;" colspan="3">
                    任务单              
                </td>
            </tr>
            <tr>
                <td align="center" width="30%">生产单位：&nbsp;<asp:TextBox ID="PT_PLANT" runat="server" /></td>
                <td align="center" width="40%">任务单编号：&nbsp;<asp:TextBox ID="PT_CODE" runat="server"  Width="128px"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="PT_CODE"></asp:RequiredFieldValidator></td>
               
                 <td  width="30%">是否需要结算：&nbsp;<asp:DropDownList ID="PT_STATE" runat="server" Width="100px">
                   <asp:ListItem Value="">-请选择-</asp:ListItem>         
                   <asp:ListItem Value="0">不需要结算</asp:ListItem>
                   <asp:ListItem Value="1">需要结算</asp:ListItem>
                   </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="PT_STATE"></asp:RequiredFieldValidator>
                   
                 </td>  
               
            </tr> 
            <tr >
            <td align="center">
                项目名称：&nbsp;<asp:TextBox ID="tbproject" runat="server"></asp:TextBox>
                 </td>
                 
               <td align="center">
                 分&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;类：&nbsp;<asp:DropDownList ID="PT_TYPE" runat="server" Width="128px">
                 <asp:ListItem Value="">-请选择-</asp:ListItem>  
                 <asp:ListItem Value="0">项目部</asp:ListItem>
                  <asp:ListItem Value="1">中材重机</asp:ListItem>
                   <asp:ListItem Value="2">港口</asp:ListItem>
                     </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="PT_TYPE"></asp:RequiredFieldValidator>
               </td>
               
              <td >
                负责部门：&nbsp;<asp:DropDownList ID="ddl_fzdep" runat="server" Width="100px" AppendDataBoundItems="True">
                   <asp:ListItem Value="">-请选择-</asp:ListItem>  
                  </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddl_fzdep"></asp:RequiredFieldValidator>
                 </td>  
                 
            </tr>  
         </table>
       </asp:Panel>  
       
    </div>
      <div>
        <table id="gr" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <asp:Repeater ID="Task_Repeater" runat="server" 
                onitemdatabound="Task_Repeater_ItemDataBound">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle">
                        <td>
                            <strong>序号</strong>
                        </td>
                        <td>
                            <strong>工程内容</strong>
                        </td>
                        <td>
                            <strong>数量</strong>
                        </td>
                        <td>
                            <strong>工程量(吨)</strong>
                        </td>
                        <td>
                            <strong>交货日期</strong>
                        </td>
                        <td>
                            <strong>图纸交货期</strong>
                        </td>                                       
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                        <td>
                        
                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                        
                        </td>
                        <td>
                        
                            <asp:TextBox ID="PT_ENGCONTEXT" runat="server" Text='<%#Eval("PT_ENGCONTEXT")%>'  ></asp:TextBox>
                          
                        </td>
                        <td>
                            <asp:TextBox ID="PT_QUANTITY" runat="server" Text='<%#Eval("PT_QUANTITY")%>' ></asp:TextBox>
                                                                     
                        </td>
                        <td>

                            <asp:TextBox ID="PT_ENGNUM" runat="server"   Text='<%#Eval("PT_ENGNUM")%>' onblur="checkEN(this)"></asp:TextBox>
                        
                        </td>
                        <td> 
                                                                 
                            <input id="PT_DLVRYDATE" runat="server" type="text"  onclick="setday(this)" value='<%#Eval("PT_DLVRYDATE")%>' readonly="readonly" />
                         
                        </td>
                        <td>
                        
                            <input id="PT_DRAWINGDATE" runat="server" type="text" onclick="setday(this)" value='<%#Eval("PT_DRAWINGDATE")%>' readonly="readonly" />
                                                                      
                        </td>                                       
                       
                    </tr>
                </ItemTemplate>
            <FooterTemplate>
                  <tr>
                       <td colspan="2" align="center">
                           合计:
                       </td>
                       <td >
                       </td>
                       <td align="center">
                          <asp:Label ID="lbSummary" runat="server" ></asp:Label>
                       </td>
                       <td>
                       </td>
                       <td> 
                       </td>
                  </tr>
          </FooterTemplate>
          </asp:Repeater>
        </table>
        </div>
        
          <br /> 
                                            
        <div> 
        
            <asp:Panel ID="panel2" runat="server" >
            <table width="100%" cellpadding="1px" cellspacing="10px" >
                <tr align="left">
                    <td  align="center"> 说明:</td>
                    <td colspan="9" valign="middle">
                      <asp:TextBox ID="tb_Comment" runat="server" Width="60%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr align="left">
                    <td  align="center"> 附件1:</td>
                    <td colspan="9" valign="middle">
                    <div  align="center" style="width: 50%">
                     <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></div>
                         <asp:Panel ID="Panel_Type_In" runat="server">
                          <div  align="center" style="width: 50%">
                              <asp:FileUpload ID="FileUploadupdate" runat="server" />
                              <asp:Button ID="bntupload" runat="server" CausesValidation="False" 
                                  OnClick="bntupload_Click" Text="上 传"  />
                              <br />
                              <asp:Label ID="lbreport" runat="server" Visible="False" ></asp:Label>
                              <br />
                              <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" 
                                  CellPadding="4" CssClass="toptable grid" DataKeyNames="fileID" 
                                  ForeColor="#333333" PageSize="5" meta:resourcekey="gvfileslistResource1">
                                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                  <AlternatingRowStyle BackColor="White" />
                                  <Columns>
                                      <asp:BoundField DataField="fileName" HeaderText="文件名称" 
                                          meta:resourcekey="BoundFieldResource1">
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="fileUpDate" HeaderText="上传时间" 
                                          meta:resourcekey="BoundFieldResource2">
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:BoundField>
                                      <asp:TemplateField HeaderText="删除" meta:resourcekey="TemplateFieldResource1">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="imgbtndelete" runat="server" Height="15px" 
                                                  ImageUrl="~/Assets/images/erase.gif" meta:resourcekey="imgbtndeleteResource1" 
                                                  OnClick="imgbtndelete_Click" ToolTip="删除" Width="15px" CausesValidation="False" />
                                          </ItemTemplate>
                                          <ControlStyle Font-Size="Small" />
                                          <HeaderStyle Width="30px" />
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="下载" meta:resourcekey="TemplateFieldResource2">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="imgbtndownload" runat="server" Height="15px" 
                                                  ImageUrl="~/Assets/images/pdf.jpg" OnClick="imgbtndownload_Click" ToolTip="下载" Width="15px" CausesValidation="False" />
                                          </ItemTemplate>
                                          <ControlStyle Font-Size="Small" />
                                          <HeaderStyle Width="30px" />
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:TemplateField>
                                  </Columns>
                                  <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" 
                                      ForeColor="White" Height="10px" />
                              </asp:GridView>
                              </div>
                          </asp:Panel>
                          
                        <asp:Panel ID="Panel_view" runat="server">
                        <div align="center" style="width: 50%">
                          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="5" 
                               CssClass="toptable grid"  CellPadding="4"   DataKeyNames="fileID" 
                               ForeColor="#333333" Width="98%">
                              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                              <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="fileName" HeaderText="名称">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fileUpDate" HeaderText="上传时间">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtndownload1" runat="server"  ImageUrl="~/Assets/images/pdf.jpg" OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Height="18px" Width="18px" />   
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
                        </asp:Panel>
                            
                    </td>
                </tr>
                <tr>
                    <td align="center">附件2:</td>
                    <td colspan="9" valign="middle">
                        <asp:Panel ID="Panel_Type_In1" runat="server">
                          <div  align="center" style="width: 50%">
                              <asp:FileUpload ID="FileUploadupdate1" runat="server" />
                              <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                  OnClick="bntupload_Click" Text="上 传"  />
                              <br />
                              <asp:Label ID="lbreport1" runat="server" Visible="False" ></asp:Label>
                              <br />
                              <asp:GridView ID="gvfileslist1" runat="server" AutoGenerateColumns="False" 
                                  CellPadding="4" CssClass="toptable grid" DataKeyNames="fileID" 
                                  ForeColor="#333333" PageSize="5" meta:resourcekey="gvfileslistResource1">
                                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                  <AlternatingRowStyle BackColor="White" />
                                  <Columns>
                                      <asp:BoundField DataField="fileName" HeaderText="文件名称" 
                                          meta:resourcekey="BoundFieldResource1">
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="fileUpDate" HeaderText="上传时间" 
                                          meta:resourcekey="BoundFieldResource2">
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:BoundField>
                                      <asp:TemplateField HeaderText="删除" meta:resourcekey="TemplateFieldResource1">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="imgbtndelete" runat="server" Height="15px" 
                                                  ImageUrl="~/Assets/images/erase.gif" meta:resourcekey="imgbtndeleteResource1" 
                                                  OnClick="imgbtndelete_Click" ToolTip="删除" Width="15px" CausesValidation="False" />
                                          </ItemTemplate>
                                          <ControlStyle Font-Size="Small" />
                                          <HeaderStyle Width="30px" />
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="下载" meta:resourcekey="TemplateFieldResource2">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="imgbtndownload" runat="server" Height="15px" 
                                                  ImageUrl="~/Assets/images/pdf.jpg" OnClick="imgbtndownload_Click" ToolTip="下载" Width="15px" CausesValidation="False" />
                                          </ItemTemplate>
                                          <ControlStyle Font-Size="Small" />
                                          <HeaderStyle Width="30px" />
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:TemplateField>
                                  </Columns>
                                  <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" 
                                      ForeColor="White" Height="10px" />
                              </asp:GridView>
                              </div>
                          </asp:Panel>
                          
                        <asp:Panel ID="Panel_view1" runat="server">
                        <div align="center" style="width: 50%">
                          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" PageSize="5" 
                               CssClass="toptable grid"  CellPadding="4"   DataKeyNames="fileID" 
                               ForeColor="#333333" Width="98%">
                              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                              <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="fileName" HeaderText="名称">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fileUpDate" HeaderText="上传时间">
                                     <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtndownload1" runat="server"  ImageUrl="~/Assets/images/pdf.jpg" OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Height="18px" Width="18px" />   
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
                        </asp:Panel>
                    </td>
                </tr>
                
                
                
                
                
                <tr align="left">
                    <td>业主负责人:</td>
                    <td >
                        <asp:TextBox ID="PT_PMCHARGERNM" runat="server" Width="80px"></asp:TextBox>
                       
                    </td>
                    <td>业主经办人:</td>
                    <td>
                        <asp:TextBox ID="PT_PMEXECUTENM" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td  >生产单位负责人:</td>
                    <td  >
                        <asp:TextBox ID="PT_PDCHARGERNM" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td  >生产单位经办人:</td>
                    <td  >
                        <asp:TextBox ID="PT_PDEXECUTENM" runat="server" Width="80px"></asp:TextBox>
                    </td>
                     <td>日期：<asp:Label ID="PT_DATE" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>
            
            </asp:Panel>
       </div>
      
      
</asp:Content>

