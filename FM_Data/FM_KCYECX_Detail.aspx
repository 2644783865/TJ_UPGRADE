<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_KCYECX_Detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_KCYECX_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>出库入库详细信息</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:White;">
    <form id="form1" runat="server">
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    <div>
     <div class="RightContentTop">
        <div class="RightContentTitle">
            <table width="100%">
                <tr>
                    
                </tr>
            </table>
           </div>
        </div>
        
                <div class="RightContent">
<div class="RightContent">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr>
                    <td align="left">物料名称：
                    <asp:Label ID="MNAME" runat="server">
                    </asp:Label>
                    </td>
                    
                    <td>起始期间：
                    <asp:Label ID="begdate" runat="server">
                    </asp:Label>
                    </td>
                    <td>截止期间：
                    <asp:Label ID="enddate" runat="server">
                    </asp:Label>
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
                  <td>
                    <asp:GridView ID="GridView5" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView5_RowDataBound" >
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="CODE" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="SI_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CHECKDATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="BILLTYPE" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_BEGNUM" HeaderText="期初数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_BEGBAL" HeaderText="期初金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>              
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                   </td>
                   </tr>
                    
                 
                 <tr>
                  <td>
                    <asp:GridView ID="GridView4" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView4_RowDataBound" >
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="CODE" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="SI_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CHECKDATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="BILLTYPE" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_BEGNUM" HeaderText="实际数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_BEGDIFF" HeaderText="期初差额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>              
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                   </td>
                   </tr>
                   
                   
                    
                    <tr>
                    <td>
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CellPadding="4" ForeColor="#333333" DataKeyNames="WG_ID" OnRowDataBound="GridView1_RowDataBound">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("WG_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WG_CODE" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="WG_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="WG_VERIFYDATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="WG_BILLTYPE" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="WG_RSNUM" HeaderText="入库数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="WG_AMOUNT" HeaderText="入库金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>         
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                  </td> 
                 </tr>
                 
                 
                 
                 <tr>
                  <td>
                    <asp:GridView ID="GridView3" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView3_RowDataBound" >
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="CODE" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="SI_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CHECKDATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="BILLTYPE" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CRCVNUM" HeaderText="实际数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CRVDIFF" HeaderText="入库差额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>              
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                   </td>
                   </tr>
                   
                   
                    
                  <tr>
                  <td>
                    <asp:GridView ID="GridView2" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CellPadding="4" ForeColor="#333333" DataKeyNames="OP_ID" OnRowDataBound="GridView2_RowDataBound">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("OP_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OutCode" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="MaterialCode" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="ApprovedDate" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="BillType" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>   
                    <asp:BoundField DataField="RealNumber" HeaderText="出库数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="出库金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>          
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                   </td>
                   </tr>
                   
                    <tr>
                  <td>
                    <asp:GridView ID="GridView6" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView6_RowDataBound" >
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="CODE" HeaderText="单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="SI_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_CHECKDATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="BILLTYPE" HeaderText="帐单类型" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_ENDNUM" HeaderText="期末数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                     <asp:BoundField DataField="SI_ENDBAL" HeaderText="期末金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>              
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
                   </td>
                   </tr>
                   
                   
                    
                    
                   
                    
                    </table>
                    <table>
                    <tr>
                    <td>
                    <asp:HiddenField ID="hfdinTotalNum" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdoutTotalNum" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdinTotalAmount" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdoutTotalAmount" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdintzTotalNum" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdintzTotalAmount" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdbegtzTotalNum" runat="server" />
                    </td>
                    <td>
                    <asp:HiddenField ID="hfdbegtzTotalAmount" runat="server" />
                    </td>
                    </tr>
                    
              </table>
                    
                  
                    
         </div>
       </div>
    </div>    

        </div><!--RightContent Part END -->
    </div>
    </form>
</body>

</html>