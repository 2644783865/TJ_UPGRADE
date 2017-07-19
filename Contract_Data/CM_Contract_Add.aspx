<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" EnableViewState="true"
    AutoEventWireup="true" CodeBehind="CM_Contract_Add.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Contract_Add" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblConForm" runat="server"></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/Setting.css" />
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../PC_Data/PcJs/pricesearch.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
//ѡ����
function SupplierSelect()
{
    var CON_TYPE=<%=ContactForm %>;        
    var i=window.showModalDialog('SupplierSelect.aspx?type='+CON_TYPE,'',"dialogHeight:500px;dialogWidth:750px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
    if(i!=null)
    {
      document.getElementById('<%=txtPCON_CUSTMNAME.ClientID%>').value=i[1];
      document.getElementById('<%=txtPCON_CUSTMID.ClientID%>').value=i[0];  
     
    }    
}
//��ӽ��㵥
function btnADDJSD_onclick(i) {
document.getElementById('<%=btnADDJSD.ClientID %>').style.display="none";//���أ���ֹ�ٴ����
var autonum=Math.round(10000*Math.random());
window.open('CM_JSD_DETAIL.aspx?condetail_id=<%=CondetailID%>&autonum='+autonum+'&Action=add');
}
//�鿴���޸Ľ��㵥
function JSDView(i){
var action=i.title;
var autonum=Math.round(10000*Math.random());
window.open('CM_JSD_DETAIL.aspx?condetail_id=<%=CondetailID%>&autonum='+autonum+'&Action='+action);

}

//������
function btnADDCR_onclick() {
if(document.getElementById("<%=lbl_pszt.ClientID %>").innerHTML=="����ͨ��")
{
document.getElementById('<%=btnADDCR.ClientID %>').style.display="none";//���أ���ֹ�ٴ����
var autonum=Math.round(10000*Math.random());
var ckb=document.getElementsByTagName("input");
var value="";
for(i=0;i<ckb.length;i++) {   
   if(ckb[i].type=="checkbox"){
      if(ckb[i].checked){
          var s = ckb[i].parentNode.parentNode.parentNode;
          var lable = s.getElementsByTagName('input');
          value += lable[1].value+'/';
      }
   }
}
value=value.substring(0, value.length - 1);
window.open('CM_CHECKREQUEST.aspx?Action=Add&autonum='+autonum+'&condetail_id=<%=CondetailID%>&contactform=<%=ContactForm %>&val='+value);
}
else
{
alert("��ͬ��δ����ͨ�����޷������");
}
}
//�޸����
function CREdit(i){
var ID=i.title;
var autonum=Math.round(10000*Math.random());  
window.open("CM_CHECKREQUEST.aspx?Action=Edit&autonum="+autonum+"&contactform=<%=ContactForm %>&CRid="+ID); 
}


//�鿴������Ϣ
function View_SP(i)
{
var url=i.title;
window.open(url); 
}

//�鿴���
function CRView(i){
var ID=i.title;
var now=new Date();
var seconds=now.getTime();  
window.showModalDialog("CM_CHECKREQUEST.aspx?Action=View&CRid="+ID+"&contactform=<%=ContactForm %>&NoUse="+seconds,obj,"dialogHeight:800px;dialogWidth:1100px");  
}


//�鿴�����¼
function FKView(i){
var ID=i.title;
var now=new Date();
var seconds=now.getTime();
window.showModalDialog("CM_PAYMENT.aspx?Action=View&PRid="+ID+"&NoUse="+seconds,obj,"dialogWidth=800px;dialogHeight=400px");  
}

//�������
function DBQK(i)
{
  var ID=i.title;
  var autonum=Math.round(10000*Math.random());
  window.showModalDialog("CM_Payment.aspx?Action=AddFK&autonum="+autonum+"&CRid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
  window.history.go(0);
}

//�鿴��Ʊ
function FPView(i){
var ID=i.title;
var now=new Date();
var seconds=now.getTime();
window.showModalDialog("CM_Bill.aspx?Action=View&BillID="+ID+"&NoUse="+seconds,obj,"dialogWidth=700px;dialogHeight=400px;");
}
//�޸ķ�Ʊ
function FPEdit(i){
var ID=i.title;
var now=new Date();
var seconds=now.getTime();
var sRet=window.showModalDialog("CM_Bill.aspx?Action=Edit&BillID="+ID+"&NoUse="+seconds,obj,"dialogWidth=700px;dialogHeight=400px;");
   if(sRet=="refresh")
   {
   //window.history.go(0);  
   }
}

//��ӷ�Ʊ
function btnAddFP_onclick() {
var now=new Date();
var seconds=now.getTime();
var sRet=window.showModalDialog('CM_Bill.aspx?Action=Add&condetail_id=<%=CondetailID%>'+"&NoUse="+seconds,obj,"dialogWidth=700px;dialogHeight=400px;");   
   if(sRet=="refresh")
   {
     window.location.reload();
   }
}

//�޸ĸ����¼ƾ֤
function Edit_PZ(i)
{
  var ID=i.title;
  var autonum=Math.round(10000*Math.random());
  window.showModalDialog("CM_Payment.aspx?Action=Edit&autonum="+autonum+"&PRid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
  window.href="CM_CheckRequestRecord.aspx";
}

 //�������ڸ�ʽ�磺2012-01-01
function dateCheck(obj)
{
    var value=obj.value;
    if(value!="")
    {
        var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
        m = re.exec(value)
        if (m == null ) 
        {
            obj.style.background="yellow";
            obj.value="";
            alert('��������ȷ��ʱ���ʽ�磺2012-01-01');
        }       
    }
 }
 
//�����
function Check_JE(obj)
{
  var t=Arabia_to_Chinese(obj);
  //���ǰ���
  var bef_chg=document.getElementById("<%=txtHTJE.ClientID %>").value;
  //������ӽ��
  var add_chg=obj.value;
  //�������
  var aft_chg=parseFloat(bef_chg)+parseFloat(add_chg)
  if(parseFloat(aft_chg)<0)
  {
     document.getElementById("<%=txtJE_CHG.ClientID %>").value="";
     alert("������ͬ���Ϊ�������޷��ύ������");
  }
  else
  {
     document.getElementById("<%=txtJE_After_CHG.ClientID %>").value=aft_chg;
  }
}

//�鿴��ͬ����
function btnRevInfo_onclick(id) {
var autonum=Math.round(10000*Math.random());
window.open('CM_ContractView_Audit.aspx?autonum='+autonum+'&Action=view&Type=1&ID='+id);
}

//�鿴����
  function View_PurOrder(orderid)
{
      var autonum=Math.round(10000*Math.random());
      window.open("../PC_Data/TBPC_Purordertotal_list.aspx?autonum="+autonum+"&TotalOrder="+orderid+"");       
}

//�鿴����Э��
function BCXYView(i){
var crid=i.title;
var autonum=Math.round(10000*Math.random());
window.open('CM_ContractView_Audit.aspx?Action=view&autonum='+autonum+'&ID='+crid+'&Type=6');

}

//��Ӳ���Э��
function add_bcxy_onclick(i) {
document.getElementById('<%=add_bcxy.ClientID %>').style.display="none";//���أ���ֹ�ٴ����
var autonum=Math.round(10000*Math.random());
window.open('CM_ContractView_Other_Add.aspx?Action=add&Type=8&autonum='+autonum+'&Conid=<%=CondetailID%>');
}

//������ͬ�ŵ���ʱ
function AutoLock() {
    lock_tip.style.display = "block";
    var t1 = 3600;
    countDown(t1);
}
function countDown(secs) {
    var hour =parseInt( secs / 3600);
    var min = parseInt( (secs - hour * 3600) / 60);
    var sec = secs - hour * 3600 -min*60
    ctl00_PrimaryContent_tip_content.innerText ="��������ʱ��"+hour+"ʱ"+min+"��"+ sec+"��";
    if (--secs >= 0) {
        setTimeout("countDown(" + secs + ")", 1000);
    }
    else {
        ctl00_PrimaryContent_tip_content.style.color = "Red";
        ctl00_PrimaryContent_TabContainer1_TabPanel1_lb_lock.innerText = "ȡ������";
        ctl00_PrimaryContent_TabContainer1_TabPanel1_lb_lock.style.color = "Red";
        
    }
}
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblAddOREdit" runat="server"></asp:Label>
                                (��<span class="red">*</span>�ŵ�Ϊ������)
                                <%--Ψһ���--%>
                                <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                <div id="lock_tip" style="display: none; color: Green">
                                    <span id="tip_content" runat="server"></span>
                                </div>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblConID" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="LinkLock" runat="server" OnClientClick="javascript:return confirm('���ȷ��Ϊ���û�����60����,��ʱ����ڲ��ᱻ������ռ��\r60���Ӻ���δ�ύ��ú�ͬ����Ȼ���ܱ�ռ��\r\rҪ����������');"
                                    OnClick="LinkLock_Click" Visible="false" CausesValidation="false">
                                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="������ͬ��" ImageUrl="~/Assets/images/lock.jpg"
                                        Height="18" Width="18" runat="server" />
                                    ������ͬ��
                                </asp:LinkButton>&nbsp;&nbsp; &nbsp;
                                <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('ȡ�����ͷ��������ú�ͬ�ſɼ���ʹ��\r\rȷ��ȡ���������رո�ҳ����');"
                                    Visible="false" CausesValidation="false" OnClick="btnNO_Click">
                                    <asp:Image ID="Image10" Style="cursor: hand" ToolTip="�������" ImageUrl="~/Assets/icons/delete.gif"
                                        Height="18" Width="18" runat="server" />
                                    �������
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnConfirm" runat="server" Text="" UseSubmitBehavior="false" OnClientClick="Javascript:if(this.value=='ȷ���޸�'){if(confirm(&quot;ȷ��Ҫ�޸ģ�&quot;)){}else return false;}else {this.value=='ȷ�����';}"
                                    OnClick="btnConfirm_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="�� ��" CausesValidation="false" OnClientClick="javascript:window.close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:Label ID="lb_addtips" runat="server" ForeColor="Red" Visible="false" Text="��ʾ�����ʱ��Ҫҳ�������ɵĺ�ͬ�Ų���������ռ�ã������������ͬ�š�����.����ǰ����������ȷ�ĺ�ͬ��"></asp:Label>
                <table width="100%">
                    <tr>
                        <td style="width: 10px">
                            <asp:Image ID="Image2" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                onClick="switchGridVidew(this,'htjbxx')" Height="15" Width="15" runat="server" />
                        </td>
                        <td>
                            ��ͬ������Ϣ
                        </td>
                        <td style="width: 40%;" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                        </asp:ToolkitScriptManager>
                                        <asp:Panel ID="palCHG" runat="server">
                                            <input id="btnJECHG" type="button" class="buttOver" runat="server" value="�����" visible="false" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnQTCHG" runat="server" Text="�������" CssClass="buttOver" OnClientClick="javascript:return confirm('�����ͬ��Ϣ��ȷ����\r\r��ʾ:\r(1) ���ڱ�ע����д�����Ϣ,�Ա���ģ�\r(2) �޸��ύ������1�������¼��');"
                                                OnClick="btnQTCHG_Click" Visible="false" /></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 40%;">
                            <asp:Label ID="lblRemind" runat="server" ForeColor="Red" Text="�����ɹ���"></asp:Label>
                            &nbsp;
                            <asp:LinkButton ID="Lbtn_ViewOrder" runat="server" OnClick="btn_ViewOrder_Click">
                                <asp:Image ID="imgToPurorder" Style="cursor: hand" ToolTip="������" ImageUrl="~/Assets/icons/dindan.jpeg"
                                    Height="17" Width="17" runat="server" />
                                ���ɹ�����
                            </asp:LinkButton>&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="btnJECHG"
                    PopupControlID="palJECHG" Position="Bottom" OffsetY="4" OffsetX="50">
                </asp:PopupControlExtender>
                <asp:Panel ID="palJECHG" CssClass="popupbox" runat="server" Visible="false">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <div class="closeLoginPanel">
                                            <a onclick="document.body.click(); return false;" title="�ر�">X</a>
                                        </div>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" Text="���ǰ���:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHTJE" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" Text="������ӽ��:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJE_CHG" runat="server" onblur="javascript:Check_JE(this);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label7" runat="server" Text="�������:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJE_After_CHG" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <asp:Button ID="btnConfirmCHG" runat="server" Text="ȷ ��" OnClick="btnJECHG_Click"
                                            OnClientClick="javascript:return confirm('�����ͬ��ȷ����\r\r��ʾ:��ȷ�����ڱ�ע����д�����Ϣ,�Ա���ģ�');" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--��ͬ������Ϣ��ʼ-->
                <div id="htjbxx" style="display: block">
                    <asp:Panel ID="palBasicInfo" runat="server">
                        <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="r_bg">
                                    ��Ŀ����:
                                </td>
                                <td class="right_bg" valign="middle">
                                    <asp:TextBox ID="tb_pjinfo" runat="server" Text=""></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    ��Ŀ���:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_PJID" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg" rowspan="3" valign="middle">
                                    �豸����(�ɶ�ѡ):
                                </td>
                                <td class="right_bg">
                                    <asp:DropDownList ID="cmbPCON_ENGNAME" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbPCON_ENGNAME_SelectedIndexChanged">
                                        <asp:ListItem>-��ѡ��-</asp:ListItem>
                                    </asp:DropDownList>
                                    <font color="#ff0000">*</font> &nbsp;&nbsp;
                                    <asp:Button ID="btn_clear_eng" runat="server" Text="�� ��" OnClientClick="return confirm('Ҫ�����ѡ������');"
                                        OnClick="btn_clear_eng_Click" />
                                </td>
                                <td class="r_bg">
                                    ���β���:
                                </td>
                                <td class="right_bg">
                                    <asp:DropDownList ID="dplPCON_RSPDEPID" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                    <font color="#ff0000">*</font> (���������ɺ�ͬ��)
                                </td>
                            </tr>
                            <tr>
                                <td class="right_bg" rowspan="2" valign="top">
                                    <asp:TextBox ID="txt_EngName" runat="server" Width="280px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBox ID="txt_ENGID" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    ������:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtORDER_ID" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    �豸���:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_ENGTYPE" runat="server"></asp:TextBox>&nbsp;
                                    <asp:DropDownList ID="ddl_engtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_engtype_Changed">
                                        <asp:ListItem Text="-��ѡ��-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="�ֽṹ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="�Ǳ��豸" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="���ڷֽ�" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="�����豸" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="�����豸" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="�ճ��豸" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="��ĥ�豸" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Ҥ��ֽ�" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Բ�ζ�ȡ�ϻ�" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="��ʽ���ϻ�" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="��ʽȡ�ϻ�" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="��ʽ���ϻ�" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="�����豸" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="����" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="�ճ��豸" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="��װ�豸" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="�����豸" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="�����豸" Value="18"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="r_bg">
                                    ǩ������:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_FILLDATE" runat="server" Text="" onchange="dateCheck(this)" />
                                    <asp:CalendarExtender ID="calender_filldate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM��,yyyy��"
                                        TodaysDateFormat="yyyy��MM��dd��" TargetControlID="txtPCON_FILLDATE">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    ��ͬ��:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BCODE" Text="" runat="server" Enabled="false" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                    <asp:Label ID="lb_lock" runat="server" Text="δ����" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                                <td class="r_bg">
                                    ��������:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_DELIVERYDATE" Text="" runat="server" onchange="dateCheck(this)" />
                                    <asp:CalendarExtender ID="calender_deliverydate" runat="server" Format="yyyy-MM-dd"
                                        DaysModeTitleFormat="MM��,yyyy��" TodaysDateFormat="yyyy��MM��dd��" TargetControlID="txtPCON_DELIVERYDATE">
                                    </asp:CalendarExtender>
                                    <font color="#ff0000">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    �Է���ͬ��:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_NAME" Text="" runat="server" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    ��ͬ���:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_JINE" runat="server" onblur="javascript:check_num(this)"
                                        Text="0"></asp:TextBox>(��) <font color="#ff0000">*</font> &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    ��ͬ���:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_TYPE" runat="server" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    ������:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BALANCEACNT" runat="server" onblur="javascript:check_num(this)"
                                        Text="0"></asp:TextBox>
                                    (��)
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    <asp:Label ID="Lbl_sup" runat="server" Text="��������:"></asp:Label>
                                </td>
                                <td class="right_bg">
                                    <input type="text" id="txtPCON_CUSTMNAME" runat="server" style="width: 280px" />
                                    <input id="txtPCON_CUSTMID" type="hidden" runat="server" />
                                    <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" onClick="SupplierSelect()"
                                        ToolTip="��Ӧ��ѡ��">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        ѡ��
                                    </asp:HyperLink>
                                    <font color="#ff0000">*</font>
                                    <br />
                                    <span style="color: Red">����ѡ�����û�иó���ʱ���ֶ���д�������޷��������������ݳ�����Ϣ</span>
                                </td>
                                <td class="r_bg">
                                    ����ɱ�:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_COST" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    ��ͬ���󵥺�:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="tb_revid" runat="server"></asp:TextBox>
                                </td>
                                <td class="r_bg">
                                    ��ͬ����״̬:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_ADUITDATE" runat="server" onclick="setday(this);" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_pszt" runat="server" Text=""></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_RevInfo" runat="server" Text="�鿴������Ϣ" OnClick="btn_RevInfo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    ������:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_RESPONSER" Text="" runat="server"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    ���������
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_SCH" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnadd" runat="server" Text="�� ��" OnClick="btnadd_Click" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="r_bg">
                                </td>
                                <td colspan="3" class="right_bg">
                                    <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">δ��ʼ</asp:ListItem>
                                        <asp:ListItem Value="1">������</asp:ListItem>
                                        <asp:ListItem Value="2">�����</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg" valign="middle">
                                    ��ע:
                                </td>
                                <td class="right_bg" colspan="3">
                                    <asp:TextBox ID="txtPCON_NOTE" runat="server" TextMode="MultiLine" Rows="5" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div style="overflow: scroll">
                                <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1" style="width: 1500px">
                                    <asp:Panel ID="Panel3" runat="server">
                                        <asp:Repeater ID="addRep" runat="server" OnItemDataBound="addRep_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle headcolor">
                                                    <td width="50px">
                                                        <strong>���</strong>
                                                    </td>
                                                    <td>
                                                        <strong>�г�����ͬ��</strong>
                                                    </td>
                                                    <td>
                                                        <strong>��Ŀ����</strong>
                                                    </td>
                                                    <td>
                                                        <strong>�豸����</strong>
                                                    </td>
                                                    <td>
                                                        <strong>����</strong>
                                                    </td>
                                                    <td>
                                                        <strong>����ͺ�</strong>
                                                    </td>
                                                    <td>
                                                        <strong>����</strong>
                                                    </td>
                                                    <td>
                                                        <strong>������</strong>
                                                    </td>
                                                    <td>
                                                        <strong>����</strong>
                                                    </td>
                                                    <td>
                                                        <strong>��Ԫ��</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                        </asp:CheckBox>
                                                        <input type="hidden" value='<%# Eval("ID")%>' />
                                                        <asp:Label ID="CM_ID" runat="server" Text='<%# Eval("ID")%>' CssClass="center" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="CM_CONTR" runat="server" Width="100px" Text='<%# Eval("CM_CONTR")%>' CssClass="center"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_PROJ" runat="server" Width="100px" Text='<%# Eval("CM_PROJ")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_ENGNAME" runat="server" Width="100px" Text='<%# Eval("CM_ENGNAME")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_MATERIAL" runat="server" Width="100px" Text='<%# Eval("CM_MATERIAL")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_MODEL" runat="server" Width="100px" Text='<%# Eval("CM_MODEL")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_NUMBER" runat="server" Width="50px" Text='<%# Eval("CM_NUMBER")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_ALL" runat="server" Width="100" Text='<%# Eval("CM_ALL")%>' CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_PRICE" runat="server" Width="100px" Text='<%# Eval("CM_PRICE")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CM_COUNT" runat="server" Width="100px" Text='<%# Eval("CM_COUNT")%>'
                                                            CssClass="center"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </table>
                                <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center">
                                    û�м�¼!</asp:Panel>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="delete" runat="server" Text="ɾ��" OnClick="delete_Click" Visible="false" />
                                        </td>
                                        <td width="95%">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td>
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <!--��ͬ������Ϣ����-->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                            <asp:TabPanel ID="Tab1" runat="server" HeaderText="��" Height="90%" Width="100%">
                                <HeaderTemplate>
                                    ��
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image4" runat="server" Height="15px" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'qk')" Style="cursor: hand" ToolTip="����" Width="15px" />
                                            </td>
                                            <td align="left">
                                                ����¼
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label1" runat="server" Text="�Ѹ����:"></asp:Label>
                                                <asp:Label ID="lblYFJE" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label2" runat="server" Text="��ͬ�ܼ�:"></asp:Label>
                                                <asp:Label ID="lblHTJE" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="֧������:"></asp:Label>
                                                <asp:Label ID="lblZFBL" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <input id="btnADDCR" type="button" runat="server" class="button-outer" value="�����"
                                                    onclick="return btnADDCR_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="qk" style="display: block">
                                        <asp:Panel ID="palQK" runat="server">
                                            <asp:GridView ID="grvQK" runat="server" AutoGenerateColumns="False" OnRowCommand="grvQK_RowCommand"
                                                CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%" OnRowDataBound="grvQK_RowDataBound"
                                                ShowFooter="True">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CR_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="���">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CR_ID" HeaderText="����">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_BQSFK" HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_USE" HeaderText="�����;">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_DATE" DataFormatString="{0:d}" HeaderText="���ʱ��">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_JBR" HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="���״̬">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("CR_STATE").ToString()=="0"?"����":Eval("CR_STATE").ToString()=="1"?"����ǩ��":Eval("CR_STATE").ToString()=="2"?"�ύ����-δ����":Eval("CR_STATE").ToString()=="3"?"�ύ����-���ָ���":"�ύ����-�Ѹ���" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�༭" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContrac" runat="server" CssClass="hand" Visible='<%#Eval("CR_STATE").ToString()=="0"?true:Eval("CR_STATE").ToString()=="1"?true:false %>'
                                                                onClick="CREdit(this);" ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />�༭
                                                            </asp:HyperLink>&nbsp;&nbsp;
                                                            <asp:LinkButton ID="btnDelete" runat="server" ToolTip="ɾ��" CausesValidation="False"
                                                                Visible='<%#Eval("CR_STATE").ToString()=="0"?true:Eval("CR_STATE").ToString()=="1"?true:false %>'
                                                                EnableViewState="true" OnClientClick="javascript:return confirm('ȷ��ɾ��������¼��');"
                                                                CommandArgument='<%# Eval("CR_ID")%>' CommandName="Del" Text="ɾ��"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�鿴">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContract" runat="server" CssClass="hand" onClick="CRView(this);"
                                                                ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Imagee1" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />�鿴
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="NoDataPanelQK" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            û�м�¼!</asp:Panel>
                                    </div>
                                    </input>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab2" runat="server" HeaderText="�������" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <asp:Panel ID="palDBQK" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image5" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'ddqk')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td align="left">
                                                    �������
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="ddqk" style="display: block">
                                            <asp:GridView ID="grvDBQK" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" ShowFooter="true">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CR_ID") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="���">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CR_ID" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="DEP_NAME" HeaderText="����">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_DATE" HeaderText="�������" DataFormatString="{0:d}" HtmlEncode="False">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_USE" HeaderText="�����;">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_BQSFK" HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContrac" runat="server" CssClass="hand" onClick="DBQK(this);"
                                                                ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Image22" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />�༭
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:Panel ID="NoDataPanelDBQK" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                û�м�¼!</asp:Panel>
                                        </div>
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image6" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'fkjl')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td>
                                                    �����¼
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="fkjl" style="display: block">
                                            <asp:GridView ID="grvFKJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="grvFK_RowDataBound">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="���">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'> </asp:Label>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PR_ID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_bczfje" runat="server" Text='<%# Eval("PR_BCZFJE") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QKQC" HeaderText="����ڴ�" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="PR_BCZFJE" HeaderText="֧�����">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PR_ZCRQ" HeaderText="֧������" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="����ƾ֤" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Cbx_FK" runat="server" Enabled="false" Checked='<%# Eval("PR_PZ").ToString()=="0"?false:true %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PR_PZH" HeaderText="ƾ֤��">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="�༭" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlFK" CssClass="hand" ToolTip='<%# Eval("PR_ID")%>' onClick='Edit_PZ(this);'
                                                                runat="server">
                                                                <asp:Image ID="img_editfk" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:Panel ID="NoDataPanelFKJL" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                û�м�¼!</asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab3" runat="server" HeaderText="��Ʊ����" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image7" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'fpjl')" Height="15" Width="15" runat="server" />
                                            </td>
                                            <td>
                                                ��Ʊ��¼
                                            </td>
                                            <td align="right">
                                                <input id="btnAddFP" type="button" value="��ӷ�Ʊ��¼" class="button-outer" runat="server"
                                                    onclick="return btnAddFP_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="fpjl" style="display: block">
                                        <asp:GridView ID="grvFP" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="grvFP_RowDataBound">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("GIV_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GIV_KPDW" HeaderText="��Ʊ��λ">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_KPRQ" HeaderText="��Ʊ����" DataFormatString="{0:d}" HtmlEncode="False">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_KPJE" HeaderText="��Ʊ���">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_FPDH" HeaderText="��Ʊ����">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_SPRQ" HeaderText="��Ʊ����" DataFormatString="{0:d}" HtmlEncode="False">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="����ƾ֤" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" Checked='<%# Eval("GIV_PZ").ToString()=="0"?false:true %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GIV_PZH" HeaderText="ƾ֤��">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_JBR" HeaderText="������">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="�༭">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hl1FP" CssClass="hand" ToolTip='<%# Eval("GIV_ID")%>' onClick='FPEdit(this);'
                                                            runat="server">
                                                            <asp:Image ID="Image113" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                            �༭</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="�鿴">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hl11FP" CssClass="hand" ToolTip='<%# Eval("GIV_ID")%>' onClick='FPView(this);'
                                                            runat="server">
                                                            <asp:Image ID="Image1212" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                            �鿴</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ɾ��">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkdel_FP" runat="server" ForeColor="Red" CommandArgument='<%# Eval("GIV_ID")%>'
                                                            OnClick="linkdel_FP_Click" OnClientClick="return confirm('ȷ��Ҫɾ���˼�¼�𣿣���');">
                    ɾ��</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Panel ID="NoDataPanelFPJL" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            û�м�¼!</asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab5" runat="server" HeaderText="���㵥" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image9" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'jsd')" Height="15px" Width="15px" runat="server" />
                                            </td>
                                            <td align="left">
                                                ���㵥��¼
                                            </td>
                                            <td align="right">
                                                <input id="btnADDJSD" type="button" runat="server" class="button-outer" value="��ӽ��㵥"
                                                    onclick="return btnADDJSD_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="jsd" style="display: block">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:GridView ID="grvjsd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CONID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="���">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CONID" HeaderText="��ͬ��">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JSDDATE" HeaderText="��ӽ��㵥����">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="�鿴">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlviewjsd" runat="server" CssClass="hand" onClick="JSDView(this);"
                                                                ToolTip="view">
                                                                <asp:Image ID="ImgViewjsd" runat="server" ImageUrl="~/Assets/images/search.gif" ToolTip='<%# Eval("CONID")%>' />�鿴
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�༭">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hleditjsd" runat="server" CssClass="hand" onClick="JSDView(this);"
                                                                ToolTip="edit">
                                                                <asp:Image ID="ImgEditjsd" runat="server" ImageUrl="~/Assets/images/modify.gif" ToolTip='<%# Eval("CONID")%>' />�༭
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ɾ��" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("CONID") %>'
                                                                OnClick="Lbtn_Del_OnClick" OnClientClick="javascript:return confirm('ȷ��Ҫɾ����');">
                                                                <asp:Image ID="ImageDel" ImageUrl="~/Assets/images/erase.gif" runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            û�м�¼!</asp:Panel>
                                    </div>
                                    </input>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab6" runat="server" HeaderText="����Э��" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'bcxy')" Height="15" Width="15" runat="server" />
                                            </td>
                                            <td align="left">
                                                ����Э���¼
                                            </td>
                                            <td align="right">
                                                <input id="add_bcxy" type="button" runat="server" class="button-outer" value="��Ӳ���Э��"
                                                    onclick="return add_bcxy_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="bcxy" style="display: block">
                                        <asp:GridView ID="GV_AddCon" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            CssClass="toptable grid" ForeColor="#333333" Width="100%" EmptyDataText="û�м�¼">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CON_ID" HeaderText="����Э����">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_XMMC" HeaderText="��Ŀ����">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_SBMC" HeaderText="�豸����">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_FBSMC" HeaderText="����">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_FBFW" HeaderText="�ְ���Χ">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_JIN" HeaderText="���" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_ZDRQ" HeaderText="��������">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="�鿴">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlviewbcxy" runat="server" CssClass="hand" onClick="BCXYView(this);"
                                                            ToolTip='<%# Eval("CR_ID")%>'>
                                                            <asp:Image ID="ImgViewbcxy" runat="server" ImageUrl="~/Assets/images/search.gif"
                                                                ToolTip='<%# Eval("CON_ID")%>' />�鿴
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
