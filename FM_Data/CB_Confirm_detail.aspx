<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="CB_Confirm_detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CB_Confirm_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<table width="100%">
<tr>
<td style="width:50%">工程量信息-<asp:Label ID="lblID" runat="server"></asp:Label></td>
<td align="center">
    <asp:Label ID="lblOpState" runat="server" Text="操作成功！" Visible="false"></asp:Label></td>
</tr>
</table>
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%"  TabStripPlacement="Top" ActiveTabIndex="0">
        
<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="市场部" TabIndex="0">
<ContentTemplate>
<div align="center">
          <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>合同号：</td>
                      <td><asp:TextBox ID="tbht" runat="server"></asp:TextBox></td>                   
                    </tr>
                    <tr align="center">
                      <td>单位：</td>
                      <td><asp:TextBox ID="tbdanwei" runat="server" ></asp:TextBox></td>                   
                    </tr>
                     <tr align="center">
                      <td>结算量：</td>
                      <td><asp:TextBox ID="tbjsl" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ControlToValidate="tbjsl" ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ValidationExpression="^[1-9]d*.d*|0.d*[1-9]d*$"></asp:RegularExpressionValidator>
                      </td>                   
                    </tr>
                     <tr align="center">
                      <td>单价：</td>
                      <td><asp:TextBox ID="tbdj" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ControlToValidate="tbdj" ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ValidationExpression="^[1-9]d*.d*|0.d*[1-9]d*$"></asp:RegularExpressionValidator>
                      </td>                   
                    </tr>
                     <tr align="center">
                      <td>结算金额：</td>
                      <td><asp:TextBox ID="tbjsje" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="tbjsje" runat="server" ErrorMessage="*" ValidationExpression="^[1-9]d*.d*|0.d*[1-9]d*$"></asp:RegularExpressionValidator>
                      </td>                   
                    </tr>
                    
                </table>
     <br />           
    <asp:Button ID="confirm1" runat="server" Text="确定" CommandName="btscb" OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"
              onclick="confirm1_Click" />

</div>

</ContentTemplate>
</cc1:TabPanel>

<cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="技术部" TabIndex="0">
<ContentTemplate>
<div align="center">
                <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>是否发运：</td>
                      <td>
                          <asp:RadioButtonList ID="rbfy" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>                   
                    </tr>                  
                     <tr align="center">
                      <td>委外是否结算：</td>
                      <td>
                          <asp:RadioButtonList ID="rbww" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem>是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>                   
                    </tr>                    
                </table>
      <br />                
    <asp:Button ID="confirm2" runat="server" Text="确定" CommandName="btjsb" onclick="confirm1_Click" OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"  />

</div>

</ContentTemplate>
</cc1:TabPanel>

         
<cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="储运部" TabIndex="0">
<ContentTemplate>
<div align="center">
          <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>是否发运：</td>
                      <td>
                          <asp:RadioButtonList ID="cfy" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>                   
                    </tr>                  
                     <tr align="center">
                      <td>运输发票是否结算：</td>
                      <td>
                          <asp:RadioButtonList ID="cysfp" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>                   
                    </tr>     
                      <tr align="center">
                      <td>发料是否录入出库系统：</td>
                      <td>
                          <asp:RadioButtonList ID="cflck" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>                   
                    </tr>                   
                </table>
    <br />            
    <asp:Button ID="confirm3" runat="server" Text="确定"  CommandName="btcyb" onclick="confirm1_Click" OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"/>
</div>


</ContentTemplate>
</cc1:TabPanel>

<cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="生产部" TabIndex="0">
<ContentTemplate>
<div align="center">
                <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>外协是否结算：</td>
                      <td>
                          <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>                   
                    </tr>                  
                </table>
    <br />            
    <asp:Button ID="confirm4" runat="server" Text="确定" CommandName="btscb1" onclick="confirm1_Click"  OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"/>

</div>

</ContentTemplate>
</cc1:TabPanel>   
<cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="采购部" TabIndex="0">
<ContentTemplate>
<div align="center">
                <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>发票是否结算：</td>
                      <td>
                          <asp:RadioButtonList ID="rblCG" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>                   
                    </tr>                  
                </table>
    <br />            
    <asp:Button ID="btnCG" runat="server" Text="确定" CommandName="CG" onclick="confirm1_Click"  OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"/>

</div>

</ContentTemplate>
</cc1:TabPanel>  
 <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="电器制造部" TabIndex="0"  >
<ContentTemplate>
<div align="center">
                <table width='60%' align="center" cellpadding="4" cellspacing="1"  border="1">
                    <tr align="center">
                      <td>发票是否结算：</td>
                      <td>
                          <asp:RadioButtonList ID="rblDQZZ" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem >是</asp:ListItem>
                             <asp:ListItem >否</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>                   
                    </tr>                  
                </table>
    <br />            
    <asp:Button ID="btnDQZZ" runat="server" Text="确定" CommandName="DQZZ" onclick="confirm1_Click"  OnClientClick="javascript:return confirm('提交后不能修改！！！\r\r确认提交吗？')"/>

</div>

</ContentTemplate>
</cc1:TabPanel>   
</cc1:TabContainer>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
   