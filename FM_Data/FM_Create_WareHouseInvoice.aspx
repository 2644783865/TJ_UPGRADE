<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/BaseMaster.master"  CodeBehind="FM_Create_WareHouseInvoice.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Create_WareHouseInvoice" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <p>下推发票</p>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script type="text/javascript" language="javascript">
//入库单详细
function ShowDetail(obj)
{
  var wg_code=obj.title;
  var time=new Date();
  var sec=time.getTime();
  window.showModalDialog("FM_Create_WareHouseInvDetail.aspx?wgcode="+wg_code+"&nouse="+sec,'',"dialogWidth=1000px;dialogHeight=580px;status=no;help=no;");
}
function btnAllDetail_onclick() {
  var time=new Date();
  var sec=time.getTime();
  window.showModalDialog("FM_Create_WareHouseInvDetail.aspx?arrayWGCode=<%=ArrayWGCode %>&nouse="+sec,'',"dialogWidth=1000px;dialogHeight=580px;status=no;help=no;");
}

</script>
<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="98%">
            <tr>
            <td align="left" style="width:50%"><strong>提示：以下入库单将下推到同一发票上</strong></td>
            <td style="width:30%" align="right">
                <input id="btnAllDetail" type="button" value="所有明细" onclick="return btnAllDetail_onclick()" />&nbsp;&nbsp;&nbsp;
                
                 
                <asp:Button ID="btnCreatInv" runat="server" Text="生成发票"                     
                    onclick="btnCreatInv_Click" />
                    
<%--           确认生成发票js    OnClientClick="javascript:return confirm('确认生成发票吗？')" 
--%>               
            </td>
            <td align="right" style="width:20%">
            <a href="FM_Invoice_Managemnt.aspx" title="返回到 发票管理界面" >返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            </tr>
            </table>
            </div>
        </div>
    </div>
    
     <div class="box-wrapper">
     <div class="box-outer">
         <asp:GridView ID="grvRK" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="编号" DataField="WG_CODE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="供应商名称" DataField="WG_SUPPLIERNM" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="日期" DataField="WG_DATE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="部门" DataField="WG_DEPNM" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="业务员" DataField="WG_BSMANNM" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="制单人" DataField="WG_ZDNM" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="收料员" DataField="WG_RMMANNM" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:TemplateField HeaderText="红蓝字" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("WG_ROB").ToString()=="0"?"红字":"蓝字" %>'></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="是否核销" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%#Eval("WG_CAVFLAG").ToString()=="0"?false:true %>' />
                </ItemTemplate>
                </asp:TemplateField> --%>
               <asp:TemplateField HeaderText="入库明细" ItemStyle-HorizontalAlign="Center">                    
                  <ItemTemplate>
                    <asp:HyperLink ID="hlContra" CssClass="hand" ToolTip='<%#Eval("WG_CODE") %>' onClick="javascript:ShowDetail(this);" runat="server">
                      <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         runat="server" />查看                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
            没有记录！
        </asp:Panel>
          </div>
     </div>
</div>       
</asp:Content>
