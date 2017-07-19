<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true" CodeBehind="CM_AddBill.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_AddBill" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <table width="100%">
    <tr>
    <td style="width:50%;">请勾选要添加发票的合同</td>
    </tr>
    </table>
<script language="javascript" type="text/javascript">
// <!CDATA[

function btnConfirm_onclick() {
var id="<%=CondetailID %>";
var form="<%=ConForm %>";
if(id=="")
  {
    alert("请选择要添加发票的合同！");
  }
else if(form==="0")
  {
     window.showModalDialog('CM_Bill_SW.aspx?Action=Add&condetail_id=<%=CondetailID%>',"obj","dialogWidth=650px;dialogHeight=350px;status:no;");   
     window.returnValue="refresh";
  }
else
  {
     window.showModalDialog('CM_Bill.aspx?Action=Add&condetail_id=<%=CondetailID%>',"obj","dialogWidth=700px;dialogHeight=400px;status:no;");   
     window.returnValue="refresh";
  }
}

//查看合同详情
function BPViewDetail(obj)
{
   var htbh_form=obj.title;
   var array=new Array();
   array=htbh_form.split("-");
   if(array[1]=="0")//商务
   {
      window.open("CM_Contract_SW_Add.aspx?Action=View&condetail_id="+array[0]);
   }
   else
   {
      window.open("CM_Contract_Add.aspx?Action=View&condetail_id="+array[0]+"&ConForm="+array[1]);
   }
}
// ]]>
</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<div class="RightContent">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr>
                    <td style="width:auto;"><strong>筛选条件</strong></td> 
                     <td align="right">
                        <input id="btnConfirm" type="button" class="button-outer" value="添 加" onclick="return btnConfirm_onclick()" /></td>                   
                    </tr>
                 </table>
             </div>
         </div>
     </div>
     
     
     <div style="height:8px" class="box_top"></div>
     <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
            <tr>
                 <td align="right" valign="bottom"><strong>项目名称:</strong></td> 
                    <td> 
                  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                     <asp:ComboBox ID="cmbPCON_PJNAME" runat="server"  AutoCompleteMode="SuggestAppend" 
                            AutoPostBack="True" DropDownStyle="DropDownList" 
                            onselectedindexchanged="cmbPCON_PJNAME_SelectedIndexChanged">
                     </asp:ComboBox></td>
                     <td align="right" valign="bottom"><strong>合同类别：</strong></td>
                     <td>
                         <asp:DropDownList ID="dplHTLB" runat="server"  AutoPostBack="true"
                             onselectedindexchanged="dplHTLB_SelectedIndexChanged">
                         <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="商务合同" Value="0"></asp:ListItem>
                         <asp:ListItem Text="委外合同" Value="1" ></asp:ListItem>
                         <asp:ListItem Text="采购合同" Value="2"></asp:ListItem>
                         <asp:ListItem Text="发运合同" Value="3"></asp:ListItem>
                         <asp:ListItem Text="其他合同" Value="4"></asp:ListItem>
                         </asp:DropDownList>
                </td> 
                <td align="right" valign="bottom"><strong>请输入合同编号：</strong></td>
                <td>
                    <asp:TextBox ID="txtHTBH" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="button-outer" Text="查 询" onclick="btnQuery_Click" />
                    </td>
        </tr>
        </table>
            <asp:GridView ID="grvHT" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
             <asp:TemplateField Visible="false">
             <ItemTemplate>
                 <asp:Label ID="lblHTBH" runat="server" Text='<%#Eval("PCON_BCODE") %>'></asp:Label>
                 <asp:Label ID="lblHTLB" runat="server" Text='<%#Eval("PCON_FORM") %>'></asp:Label>
               </ItemTemplate>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>   
           <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                 <asp:CheckBox ID="cbxState" runat="server" AutoPostBack="True"  oncheckedchanged="cbxState_CheckedChanged" />
               </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField HeaderText="合同编号" DataField="PCON_BCODE" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField HeaderText="合同名称" DataField="PCON_NAME" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField HeaderText="所属项目" DataField="PCON_PJNAME" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField HeaderText="所属工程" DataField="PCON_ENGNAME" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField HeaderText="合同金额" DataField="PCON_JINE" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField HeaderText="支付金额" DataField="PCON_YFK" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:TemplateField HeaderText="合同类别" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%#Eval("PCON_FORM").ToString()=="0"?"商务":Eval("PCON_FORM").ToString()=="1"?"委外":Eval("PCON_FORM").ToString()=="2"?"采购":Eval("PCON_FORM").ToString()=="3"?"发运":"其他" %>'></asp:Label>
            </ItemTemplate>

    <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="详细" ItemStyle-Width="80px">
             <ItemTemplate>
              <asp:HyperLink ID="hlContract" runat="server" CssClass="hand" onClick="BPViewDetail(this);" ToolTip='<%# Eval("PCON_BCODE").ToString()+"-"+Eval("PCON_Form").ToString()%>'>
               <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/res.gif" /> 
                 查看</asp:HyperLink>
                 </ItemTemplate>
                 <ItemStyle HorizontalAlign="Center" />
             </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />     
            </asp:GridView>  
        <asp:Panel ID="NoDataPanel" runat="server"  HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
        <uc1:UCPaging ID="UCPaging1" runat="server" />       
        </div>
     </div> 
    </div>
</asp:Content>

