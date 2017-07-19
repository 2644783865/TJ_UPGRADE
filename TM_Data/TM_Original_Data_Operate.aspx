<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Original_Data_Operate.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_Original_Data_Operate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>ԭʼ�����޸�</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/TMOrgInput.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        function window.confirm(str1, str2) {
            execScript("n   =   msgbox('" + str1 + "'&vbCrlf&vbCrlf+'��ʾ:'&vbCrlf&vbCrlf+'" + str2 + "',273,'��Ϣ��ʾ')", "vbscript");
            if (+n == 1) {
                return true;
            }
            else if (+n == 2) {
                return false;
            }
        }

        function SubmitConfirm(obj) {
            if (obj.value == "�޸�") {
                return window.confirm("ȷ���޸���", "�¼�Ҳ���޸�,�磺3.2>3.3����3.2.1>3.3.1");
            }
        }



        function showtip() {
            window.showModalDialog("TM_Original_Data_Operate_sptip.aspx", '', "dialogHeight:200px;dialogWidth:500px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        }

        function SubmitData(obj) {
           
            if (CheckNumNotZeroWithPurUnit(2)) {
              obj.disabled = true;
//                __doPostBack("btnConfirm", "");
          }
       
        }

        function CalTuUnitRealWgt(obj) {
            var txt = obj.value;
            var pattem = /^[0-9]+(\.[0-9]+)?$/;
            if (pattem.test(txt)) {
                var shuling = document.getElementById("shuliang").value;
                document.getElementById("txtTuZzXuhao").value = (parseFloat(txt) * shuling).toFixed(2);
            }
            else {
                alert("��������ȷ����ֵ��ʽ������");
                obj.value = "0";
            }
        }

        function CalWeight(obj) {
            obj.disabled = true;
            document.getElementById("btnSigCalWeight").disabled = true;
            document.getElementById("btnConfirmReal").disabled = true;
            __doPostBack("btnCalWeightHidden", "");
        }

        function SigCalWeight(obj) {
            obj.disabled = true;
            document.getElementById("btnCalWeight").disabled = true;
            document.getElementById("btnConfirmReal").disabled = true;
            __doPostBack("btnSigCalWeightHidden", "");
        }

        function getClientId() {
            var paraId5 = '<%= txtBxishu.ClientID %>';
            var paraId6 = '<%= txtXxishu.ClientID %>';
            return { BXishu: paraId5, XXishu: paraId6 }; //���ɷ�����
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" style="width: 900px;">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblXishu" runat="server" Text="" ForeColor="Red"
                                    Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right">
                                <strong>�ƻ�ϵ��:</strong>(��)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("��������ȷ����ֵ������");this.value="1.1";this.foucs();}'
                                    style="width: 30px" value="1.1" />(��/Բ��)<input id="txtXxishu" runat="server" style="width: 30px"
                                        onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("��������ȷ����ֵ������");this.value="1.05";this.foucs();}'
                                        type="text" value="1.05" />(����)<input id="txtQxishu" runat="server" style="width: 30px"
                                            disabled="disabled" type="text" value="1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td class="tdleft1">
                                ���ϱ���:
                            </td>
                            <td class="tdright1" colspan="3" >
                                <asp:TextBox ID="marid" runat="server" onchange="modifyCode()"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionSetCount="10" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                    ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True">
                                </cc1:AutoCompleteExtender>
                                <font color="#ff0000">*</font>
                                <asp:Button ID="btnAddMarID" runat="server" OnClick="btnAddMarID_OnClick" Text="������ϱ���" />
                                &nbsp;
                                <input id="hdmarid" type="text" runat="server" readonly="readonly" style="display: none" />
                                &nbsp;&nbsp;
                                <asp:Label ID="lblTip" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"
                                    Text="����л򲵻�,�����޷��޸�..."></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblAlt" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"
                                    Text="�����á�"></asp:Label>
                                    </td>
                             
                          
                        </tr>
                        <tr>
                        <td class="tdleft1">
                                ����:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cnname" runat="server" ></asp:TextBox>
                                <input id="hdcnname" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>

                            <td class="tdleft1">
                                ͼ��:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="tuhao" runat="server"></asp:TextBox>
                                <input id="hdtuhao" type="text" runat="server"  style="display: none" /><font
                                    color="#ff0000">*</font>
                            </td>
                        </tr>
                        <tr>
                                                    <td class="tdleft1">
                                ����:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="zongxu" runat="server" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                                <div>
                                    <asp:Button ID="btnChangeZX" runat="server" Text="�޸�" OnClientClick="return SubmitConfirm(this);"
                                        CommandName="ActivateIndex" OnClick="btnChangeZX_OnClick" />
                                    <input id="hdzongxu" type="text" runat="server" readonly="readonly" style="display: none" />&nbsp;&nbsp;&nbsp;<span
                                        runat="server" id="div_2" style="color: Red;"><asp:CheckBox ID="ckbWchgZX" runat="server"
                                            Checked="true" />�����¼�Ҳ�޸�</span>
                                </div>
                            </td>
                             <td class="tdleft1">
                                ��̨����|̨��:<br />
                                ������|�ƻ�����:
                            </td>
                            <td class="tdright1">
                                <input id="sing_shuliang" type="text" runat="server" onchange="modifyCalculation(3)" 
                                    title="��̨����" style="width: 50px;" /><font color="#ff0000">*</font>
                                <input id="hdshuliang" type="text" runat="server" onfocus="this.blur();" readonly="readonly"
                                    style="display: none; width: 50px;" />
                                |&nbsp;&nbsp;&nbsp;<input id="taishu" type="text" runat="server" title="̨��" style="width: 50px;
                                    background-color: #EEEEEE" /><br />
                                <asp:TextBox ID="shuliang" runat="server" onfocus="this.blur();" BackColor="#EEEEEE"
                                    title="������" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;<asp:TextBox
                                        ID="p_shuliang" runat="server" ToolTip="�ƻ�����" onchange="ModifyChangePnum(this);"
                                        Width="50px"></asp:TextBox><font color="#ff0000">*</font><input id="hd_p_shuliang"
                                            type="text" runat="server" readonly="readonly" style="display: none; width: 50px;" />
                            </td>
                            
                           
                        </tr>
                        <tr>
                            <td class="tdleft1">
                                ���ϳ���(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaocd" runat="server" onchange="modifyCalculation(1)"></asp:TextBox><font
                                    color="#ff0000">*</font>&nbsp;&nbsp;<input id="ckbYL" checked="checked" style="display: none;"
                                        title="��ѡ����������" type="checkbox" />
                                <input id="hdcailiaocd" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                ���Ͽ��(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaokd" runat="server" onchange="modifyCalculation(2)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaokd" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                        
                          <td class="tdleft1">
                                ���ϱ�ע:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="xlNote" runat="server" onchange="modifyCalculation(2)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdxlNote" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                           
                            <td class="tdleft1">
                                �����ܳ�(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaozongchang" runat="server" onchange="Calculation(4);"></asp:TextBox>
                                <input id="hdcailiaozongchang" type="text" runat="server" readonly="readonly" style="display: none" /><font
                                    color="#ff0000">*</font>
                            </td>
                        </tr>
                         <tr>
                           <td class="tdleft1">
                                �������:
                            </td>
                            <td class="tdright1">
                               
                                <asp:DropDownList ID="cailiaoType" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="��" Value="��"></asp:ListItem>
                                 <asp:ListItem Text="��" Value="��"></asp:ListItem>
                                  <asp:ListItem Text="Բ" Value="Բ"></asp:ListItem>
                                   <asp:ListItem Text="��" Value="��"></asp:ListItem>
                                 <asp:ListItem Text="��" Value="��"></asp:ListItem>
                                  <asp:ListItem Text="��" Value="��"></asp:ListItem>
                                   <asp:ListItem Text="�ɹ���Ʒ" Value="�ɹ���Ʒ"></asp:ListItem>
                                    <asp:ListItem Text="�ǽ���" Value="�ǽ���"></asp:ListItem>   
                                </asp:DropDownList>
                                
                                &nbsp;&nbsp;&nbsp;
                                <input id="hdcailiaoType" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                          
                            <td class="tdleft1">
                                ����|�ƻ�����(m <sup>2</sup>):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="my" runat="server" Width="70px" onchange="Calculation(3)"></asp:TextBox>
                                |&nbsp;<asp:TextBox ID="bgzmy" runat="server" Width="70px" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("��������ȷ����ֵ������");this.value="0";this.select();}}'></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdbgzmy" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        
                        <tr>
                             <td class="tdleft1">
                                ͼֽ�ϵ���(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTuDz" onchange="CalTuUnitRealWgt(this);" runat="server"></asp:TextBox>
                               
                               <input id="hidTudz" type="hidden" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                ͼֽ������(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTuZz" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                       <td class="tdleft1">
                                ��λ:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="techUnit" runat="server" Enabled="false" ToolTip="(������λ)-(�ɹ���λ)"></asp:TextBox>
                                <input id="hidtechunit" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">��������:</td><td class="tdright1"><asp:TextBox ID="txtYongliang" runat="server" ></asp:TextBox>
                                <input id="hidYongliang" type="text" runat="server" readonly="readonly" style="display: none" /></td>
                        </tr>
                         <tr>
                            <td class="tdleft1">
                                ���ϵ���(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaodzh" runat="server" onchange="Calculation(1)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaodzh" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                ��������(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaozongzhong" onchange="ChangeMarWeight(this);" runat="server"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaozongzhong" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdleft1">
                                ��������(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="lilunzhl" runat="server" Enabled="false" ToolTip="��������ʱ�Զ��滻,�޷��ֶ��޸�"></asp:TextBox>
                                <input id="hdlilunzhl" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            
                             <td class="tdleft1">
                                ����:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="biaozhun" runat="server" Enabled="false" ToolTip="��������ʱ�Զ��滻,�޷��ֶ��޸�"></asp:TextBox>
                                <input id="hdbiaozhun" type="text" runat="server" readonly="readonly" title="��������ʱ�Զ��滻,�޷��ֶ��޸�"
                                    style="display: none" />
                            </td>
                            
                            
                        </tr>
                       
                        <tr>
                         <td class="tdleft1">
                                ���Ϲ��:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaoguige" runat="server" Enabled="false" ToolTip="��������ʱ�Զ��滻,�ֶ��޸���Ч"></asp:TextBox>
                                <input id="hdcailiaoguige" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        
                            <td class="tdleft1">
                                ����:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="caizhi" runat="server" Enabled="false" ToolTip="��������ʱ�Զ��滻,�޷��ֶ��޸�"></asp:TextBox>
                                <input id="hdcaizhi" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                          
                        </tr>
                          <tr>
                         <td class="tdleft1">
                                ���Ϸ�ʽ:
                            </td>
                            <td class="tdright1">
                                 <asp:TextBox ID="xialiao" runat="server"></asp:TextBox>
                                <input id="hdxialiao" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        
                            <td class="tdleft1">
                                ��������:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtProcess" runat="server"></asp:TextBox>
                                <input id="hdtxtProcess" type="text" runat="server"  style="display: none" />
                            </td>
                          
                        </tr>
                        <tr>
                           
                             <td class="tdleft1">
                                �Ƿ񶨳�:
                            </td>
                            <td class="tdright1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblSFDC" runat="server" RepeatColumns="2" onclick="ModifyFix(this);"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <font color="#ff0000">*</font>
                                        </td>
                                    </tr>
                                </table>
                                <input id="hdtxtSFDC" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                           
                             <td class="tdleft1">
                                ͼֽ�ϲ���:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTZCZ" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                           
                         
                        </tr>
                              <tr>
                            <td class="tdleft1">
                                ͼֽ������:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTZWT" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                <input id="Text1" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                
                        <tr>
                            <td class="tdleft1" rowspan="2">
                                <asp:Label ID="lblMS" runat="server" Text="������ϸ:"></asp:Label>
                            </td>
                            <td class="tdright1" colspan="3">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:RadioButtonList ID="rblMSSTA" runat="server" Enabled="false" RepeatColumns="2"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="��" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 30%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblInMS" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="����" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="������" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <font color="#ff0000">*</font>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <input id="txthdinms" type="text" runat="server" readonly="readonly" style="display: none" />
                                        <td style="width: 50%">
                                            <asp:RadioButtonList ID="rblMSRew" Enabled="false" runat="server" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="δ�ύ" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="�����" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ͨ��" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="����" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblMSChangeSta" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="ɾ��" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="�޸�" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="����" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMSRew" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        
                        </tr>
                        <tr>
                            <td class="tdleft1" rowspan="2">
                                <asp:Label ID="lblMP" runat="server" Text="���ϼƻ�:"></asp:Label>
                            </td>
                            <td class="tdright1" colspan="3">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:RadioButtonList ID="rblMPSTA" runat="server" ToolTip="�����ύ���ϼƻ�������£����ϼƻ��Ƿ��ύ"
                                                Enabled="false" RepeatColumns="2" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="��" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:DropDownList ID="ddlWMarPlan" ToolTip="������¼�Ƿ����ύ���ϼƻ�" runat="server">
                                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:CheckBox ID="ckbWMarplanToLower" runat="server" />�������¼�
                                        </td>
                                       
                                        <td style="width: 40%">
                                            <asp:RadioButtonList ID="rblMPRew" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="δ�ύ" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="�����" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ͨ��" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="����" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                      
                                        <td colspan="2">
                                        
                                        
                                        <table width="100%" >
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblMPChangSta" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="ɾ��" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="�޸�" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="����" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPRew" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                        
                                          
                        </tr>
  
                  

                        <tr>
                            <td class="tdleft1">
                                ��ע:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="beizhu" runat="server" TextMode="MultiLine" Height="50px" Width="70%"></asp:TextBox>
                                <input id="hdbeizhu" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                      <%--  <tr>
                            <td class="tdleft1">
                                ������Ϣ:<br />
                                (�Լ�¼��˵����Ϣ)
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtOtherNote" runat="server" TextMode="MultiLine" Height="50px"
                                    Width="70%"></asp:TextBox>
                                <input id="Text2" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdleft1">
                                ���:
                            </td>
                            <td colspan="3">
                                <table width="100%" class="toptable grid">
                                    <tr class="toptable grid">
                                        <td align="center">
                                            <asp:Button ID="btnMarChange" runat="server" Text="����/����/�����޸�" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" BorderStyle="None" CommandName="wszChg" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('ȷ�ϱ����','������ɺ��޷�ȡ��,����ϸ�˶ԣ�����')" />
                                            <br />
                                            <asp:CheckBox ID="ckbMarChange" runat="server" ToolTip="������ţ����������������仯ʱ���Ӽ�Ҳ�������޸�����" />����������¼�
                                        </td>
                                      <%--  <td align="center">
                                            <asp:Button ID="btnAttChange" runat="server" Text="�ṹ�޸�" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" BorderStyle="None" CommandName="attChg" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('���������������ȷ�Ͻṹ�����','������ɺ��޷�ȡ��,����ϸ�˶ԣ�����');" />
                                            <br />
                                            <asp:CheckBox ID="ckbAttChange" runat="server" ToolTip="������ű����" />����������¼�
                                        </td>--%>
                                   <td align="center">
                                            <asp:Button ID="btnDelete" runat="server" Text="����ɾ��" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" CommandName="singleDelete" BorderStyle="None" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('���������������ȷ�����ɾ����','������ɺ��޷�ȡ��,����ϸ�˶ԣ�����');" />
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnattDelete" runat="server" Text="�ṹɾ��" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" CommandName="attDelete" BorderStyle="None" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('���������������ȷ�����ɾ����','������ɺ��޷�ȡ��,����ϸ�˶ԣ�����');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdleft1">
                            </td>
                            <td colspan="3">
                             <%-- <input id="btnConfirmReal"  type="button" style="width: 40" value="ȷ��" runat="server" onclick="SubmitData(this);"  />--%>
                         <asp:Button ID="btnConfirmReal" runat="server" Text="ȷ��"  Width="40px" OnClick="btnConfirm_Click" />
                                
                         <%--  <asp:Button ID="btnConfirm" UseSubmitBehavior="true" runat="server" CssClass="hidden"  Width="0" Text="ȷ��" OnClick="btnConfirm_Click" />--%>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="ȡ��" Width="40" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="confirmChange" runat="server" Text="������" Width="55" OnClick="confirmChange_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelMSChange" runat="server" Text="ȡ����ϸ���" Width="80" OnClick="btnCancelMSChange_OnClick"
                                    OnClientClick="return window.confirm('ȷ�ϼ�����','��ȡ����ϸ����������������ϸ����ˡ�����ȷ�ϸ�����¼�Ƿ���ȷ������Ҫע����Ǹü�¼������');" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelMpChange" runat="server" Text="ȡ���ƻ����" Width="80" OnClick="btnCancelMpChange_OnClick"
                                    OnClientClick="return window.confirm('ȷ�ϼ�����','��ȡ���ƻ������ʱ���뱣֤��ǰ��¼�����ύ�ƻ���¼һ�£�����');" />
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: relative; bottom: 50%; right: -20%">
                <table width="60%">
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            ���ݴ����У����Ժ�...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
