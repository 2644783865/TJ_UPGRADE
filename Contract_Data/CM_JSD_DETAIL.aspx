<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PopupBase.Master" CodeBehind="CM_JSD_DETAIL.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_JSD_DETAIL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="RightContentTitlePlace" ID="Title_Content">
 结算单
</asp:Content>
 <asp:Content runat="server" ContentPlaceHolderID="PrimaryContent" ID="Main_Content">
  <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />
  <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
  
  
  <script type="text/javascript" language="javascript">
   //打印
function Print() {

window.open('CM_JSD.aspx?condetail_id=<%=CondetailID%>');
}

function HideSaveBtn(){
document.getElementById('<%=btn_Save.ClientID %>').style.display="none";//隐藏，防止再次保存
}

//检查输入格式
function checkRN(tb)
        {           
            var realnum = parseFloat(tb.value);
            
            if (isNaN(realnum))
            {
                alert('请输入正确的数字！');                
                tb.value = 0;
            }
}

//根据结算数据调整改变结算金额调整的值
function ChangeBalance(object)
{
       var realnum = parseFloat(object.value);
            
            if (isNaN(realnum))
            {
                alert('请输入正确的数字！');                
                tb.value = 0;
            }
            else
            {                
                var tbrow=object.parentNode.parentNode;  //tr
                var input_balance=tbrow.getElementsByTagName("td")[11].getElementsByTagName("input")[0]; //结算金额调整
                var input_unitprice=tbrow.getElementsByTagName("td")[11].getElementsByTagName("input")[3]; //合同单价
                
                input_balance.value=parseFloat(realnum*input_unitprice.value);
            }
  
}
</script>
<style>
    #tbbase input
    {  
    	border-top: 0;
        border-left: 0;
        border-right: 0;        
        
    }
</style>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="tbbase">
     <table width="100%">
      <tr>
      <td width="100px">采购合同名称:</td>
      <td>
          <asp:Label ID="lbl_HTMC" runat="server" Text=""></asp:Label></td>
      <td width="100px">合同编号:</td>
      <td>
          <asp:Label ID="lbl_HTBH" runat="server" Text=""></asp:Label></td>
      <td width="100px">供货商名称:</td>
      <td>
          <asp:Label ID="lbl_SUPNAME" runat="server" Text=""></asp:Label>          
          </td>
          <td >
              <asp:Button ID="btn_Save" runat="server" Text="保 存" OnClick="btn_Save_Click"/></td>
      </tr>
      <tr>
      <td>结算日期:</td>
      <td>
          <asp:Label ID="lbl_JSDATE" runat="server" Text=""></asp:Label></td>
      <td>开户行:</td>
      <td>          
          <asp:TextBox ID="tb_khh" runat="server" Width="180px" ></asp:TextBox>
          
          </td>
      <td>帐号:</td>
      <td>          
          <asp:TextBox ID="tb_zh" runat="server" Width="180px" ></asp:TextBox>
          </td>
          <td>
             <asp:HyperLink ID="HyperLink1" runat="server" onclick="javascript:Print();" CssClass="hand">
   <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif" title="打印"/>
  打 印</asp:HyperLink></td>
      </tr>
     </table>
     
      <asp:GridView ID="grvjsd" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                     <AlternatingRowStyle BackColor="White" /> 
                        <Columns>
                           <asp:BoundField DataField="marnm" HeaderText="供货名称">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField> 
                             <asp:BoundField DataField="margg" HeaderText="规格">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="marcz" HeaderText="材质">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="margb" HeaderText="国标">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="marunit" HeaderText="单位">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="zxnum" HeaderText="合同数量">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="ctprice" HeaderText="合同单价" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ctamount" HeaderText="合同金额" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="recgdnum" HeaderText="实际结算数量 ">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sjamount" HeaderText="实际结算金额" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="结算数量调整" >
                               <ItemTemplate>
                                <asp:TextBox ID="tb_addnum" runat="server" Text='<%# Eval("addnum") %>' onchange="ChangeBalance(this)" Width="60px"></asp:TextBox>
                               </ItemTemplate>                            
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="结算金额调整" >
                               <ItemTemplate>
                                <asp:TextBox ID="tb_balance" runat="server" Text='<%# Eval("balance") %>' onchange="checkRN(this)" Width="60px"></asp:TextBox>
                               <asp:HiddenField ID="hdmarid" runat="server" Value='<%# Eval("marid") %>' />
                               <asp:HiddenField ID="hdhtnum" runat="server" Value='<%# Eval("zxnum") %>' />
                               <asp:HiddenField ID="hdhtunitprice" runat="server" Value='<%# Eval("ctprice") %>' />
                               <asp:HiddenField ID="hdhtsummoney" runat="server" Value='<%# Eval("ctamount") %>' />
                               <asp:HiddenField ID="hdinputnum" runat="server" Value='<%# Eval("recgdnum") %>' />
                               <asp:HiddenField ID="hdjsmoney" runat="server" Value='<%# Eval("sjamount") %>' />                               
                               </ItemTemplate>                            
                            </asp:TemplateField>                        
                            
                             <asp:TemplateField HeaderText="备注" >
                               <ItemTemplate>
                                <asp:TextBox ID="tb_note" runat="server" Text='<%# Eval("bezhu") %>'></asp:TextBox>
                               </ItemTemplate>                            
                            </asp:TemplateField>
                             </Columns>
                     <EditRowStyle BackColor="#2461BF" />
                     <FooterStyle  BackColor="White" Font-Bold="True" ForeColor="Blue" 
                                   HorizontalAlign="Center" /> 
                     <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                     <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                   <span style="color:Red">提示：如需调整结算数量，可在【结算数量调整】中填适当的值，如不需要改数量，直接在【结算金额调整】上进行调整，正数为增加，负数为减少</span> 
            <hr />
             <table width="100%" class="tabGg" cellpadding="0" cellspacing="0">
              <tr style="height:30px">
              <td class="r_bg" style="text-align:center">结算金额</td>
              <td class="right_bg"  style="text-align:center">                 
                  <asp:Label ID="lbl_JSJE" runat="server" Text="" ></asp:Label>
                  </td>
              <td class="r_bg" style="text-align:center">结算金额大写</td>
               <td class="right_bg" style="text-align:center">
                  <asp:Label ID="lbl_JSJEDX" runat="server" Text=""></asp:Label></td>
              </tr>          
             
             </table>       
    </div>
     </ContentTemplate>
     </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
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