<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Original_Data_Edit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Original_Data_Edit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>ԭʼ�����޸�</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
</head>
<body>

<JSR:JSRegister ID="JSRegister1" runat="server" />
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr><td> ԭʼ����(��<span class="red">*</span>�ŵ�Ϊ������)</td></tr>
                 </table>
             </div>
         </div>
     </div>
    <div class="box-wrapper">
        <div class="box-outer">
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                <td class="tdleft1">���ϱ���:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="marid" runat="server" onblur="modifyCode()"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" 
                        CompletionSetCount="10" DelimiterCharacters="" Enabled="True"
                        MinimumPrefixLength="1" ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" 
                        TargetControlID="marid" UseContextKey="True">
                    </cc1:AutoCompleteExtender>
                    <font color="#ff0000">*</font>
                    <input id="hdmarid" type="text" runat="server" readonly="readonly" style="display: none" />
                </td>
                <td class="tdleft1">��������:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaoname" runat="server" ></asp:TextBox>
                        <input id="hdcailiaoname" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
               </tr>
                
                <tr>
                 <td class="tdleft1">����:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="zongxu" runat="server"></asp:TextBox>
                    <input id="hdzongxu" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">���Ϲ��:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaoguige" runat="server" ></asp:TextBox>
                    <input id="hdcailiaoguige" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                </tr>
                
                <tr>
                <td class="tdleft1">��������:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cnname" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKeyComs" runat="server">
                    <asp:ListItem Text="--    --" Value=""></asp:ListItem>
                    <asp:ListItem Text="�ؼ�����" Value="�ؼ�����"></asp:ListItem>
                    </asp:DropDownList>
                    <input id="hdcnname" type="text" runat="server" readonly="readonly" style="display: none"/>
                    <input id="hdkeycoms" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">Ӣ������:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="egname" runat="server"></asp:TextBox>
                    <input id="hdegname" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                </tr>
                
                <tr>
                <td class="tdleft1">���ϳ���:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaocd" runat="server" onblur="modifyCalculation(1)"></asp:TextBox>
                    <input id="hdcailiaocd" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">���:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="guige" runat="server" ></asp:TextBox>
                    <input id="hdguige" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>      
                </tr>
    
                <tr>
                <td class="tdleft1">���Ͽ��:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaokd" runat="server" onblur="modifyCalculation(2)"></asp:TextBox>
                        <input id="hdcailiaokd" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
               <td class="tdleft1">�����ܳ�:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaozongchang" runat="server"></asp:TextBox>
                    <input id="hdcailiaozongchang" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td> 
                </tr>
                
                 <tr>
                    <td class="tdleft1">����:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="shuliang" runat="server" onblur="modifyCalculation(3)"></asp:TextBox>
                        <input id="hdshuliang" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">��������:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="lilunzhl" runat="server" ></asp:TextBox>
                        <input id="hdlilunzhl" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                 </tr>
                
                <tr>
                    <td class="tdleft1">���ϵ���:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaodzh" runat="server" onblur="Calculation(1)"></asp:TextBox>
                        <input id="hdcailiaodzh" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">��λ:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="unit" runat="server" ></asp:TextBox>
                        <input id="hdunit" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="tdleft1">����(m2):</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="bgzmy" runat="server" onblur="Calculation(3)"></asp:TextBox>
                        <input id="hdbgzmy" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">��������:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaozongzhong" runat="server"></asp:TextBox>
                        <input id="hdcailiaozongzhong" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                </tr>
                 
                 <tr>
                    <td class="tdleft1">����:</td>     
                    <td class="tdright1">
                       <asp:TextBox ID="caizhi" runat="server"></asp:TextBox>
                       <input id="hdcaizhi" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">ë��:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="xinzhuang" runat="server"></asp:TextBox>
                        <input id="hdxinzhuang" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr>
                    <td class="tdleft1">����:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="dzh" runat="server" onblur="Calculation(2)"></asp:TextBox>
                        <input id="hddzh" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">����:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="zongzhong" runat="server"></asp:TextBox>
                        <input id="hdzongzhong" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr>
                    <td class="tdleft1">��׼:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="biaozhun" runat="server"></asp:TextBox>
                        <input id="hdbiaozhun" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">״̬:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="zhuangtai" runat="server"></asp:TextBox>
                        <input id="hdzhuangtai" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 <tr>
                   <td class="tdleft1">�Ƿ�����������ϸ:</td>
                   <td class="tdright1">
                       <asp:RadioButtonList ID="rblSFZZMX" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                          <asp:ListItem Text="��" Value="1"></asp:ListItem>
                          <asp:ListItem Text="��" Value="0"></asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
                   <td>�Ƿ񶨳�:</td>
                   <td class="tdright1">                       
                       <asp:RadioButtonList ID="rblSFDC" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                          <asp:ListItem Text="��" Value="1"></asp:ListItem>
                          <asp:ListItem Text="��" Value="0"></asp:ListItem>
                       </asp:RadioButtonList>
                       <input id="hdtxtSFDC" type="text" runat="server" readonly="readonly" style="display: none" />
                   </td>
                 </tr>
                 <tr>Z
                   <td class="tdleft1">��������</td>
                   <td class="tdright1" colspan="3">
                       <asp:TextBox ID="txtProcess" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                       <input id="hdtxtProcess" type="text" runat="server" readonly="readonly" style="display: none" />
                   </td>
                 </tr>
                 <tr>
                    <td class="tdleft1">��ע:</td>     
                    <td colspan="3">
                        <asp:TextBox ID="beizhu" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                        <input id="hdbeizhu" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr id="change" runat="server" visible="false">
                    <td class="tdleft1">���״̬:</td>     
                    <td class="tdright1">
                        <asp:RadioButtonList ID="rblstatus" RepeatColumns="3" runat="server"  RepeatDirection="Horizontal"
                            AutoPostBack="true">                
                        <asp:ListItem Text="���ϼƻ�" value="0"></asp:ListItem>
                        <asp:ListItem Text="������ϸ" Value="1"></asp:ListItem>
                        <asp:ListItem Text="������Э" Value="2" ></asp:ListItem>
                    </asp:RadioButtonList>
                    <td class="tdleft1">ɾ��:</td> 
                    <td class="tdright1">
                       <asp:LinkButton ID="delete" runat="server" 
                            OnClientClick="return confirm(&quot;ȷ��ɾ������ʾ�����ͨ����ɾ������ԭʼ���ݣ�&quot;)" Font-Underline="True" 
                            ForeColor="Blue">����ɾ��</asp:LinkButton> 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="attChange" runat="server" 
                            OnClientClick="return confirm(&quot;ȷ��ɾ����\r\r��ʾ�����ͨ����ɾ���ýṹ������ԭʼ���ݣ�&quot;)" Font-Underline="True" 
                            ForeColor="Blue">�ṹɾ��</asp:LinkButton>
                    </td>
                    </td>
                 </tr>
            
                <tr>
                    <td class="tdleft1"></td>   
                    <td colspan="3">
                    <asp:Button ID="btnConfirm" runat="server" Text="ȷ ��" onclick="btnConfirm_Click"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="ȡ ��" onclick="btnCancel_Click" /> 
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="confirmChange" runat="server" Text="�� ��" onclick="confirmChange_Click" />
                    <%--<asp:LinkButton ID="confirmChange" runat="server" Font-Underline="True" 
                            ForeColor="Blue" onclick="confirmChange_Click" >�� ��</asp:LinkButton>--%>                    
                    </td>
                 </tr>
                 
            </table>
         </div>
       </div>
       <div>
            <input id="waixie" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="mpstate" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="msstate" type="text" runat="server" readonly="readonly" style="display: none" />
       </div>
     </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
