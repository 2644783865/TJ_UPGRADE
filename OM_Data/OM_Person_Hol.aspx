<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_Person_Hol.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Person_Hol" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ���ͳ��
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script language="javascript" type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#tab input[type=checkbox]").attr("checked","true");
}
else{
 $("#tab input[type=checkbox]").removeAttr("checked");
}
});})//jquery��д����������һ��������Ȼ��׽�����¼��Ķ��󣬴����ö���ʱִ�е��¼���������������ĳЩ�ض��Ŀؼ����ж϶����Ƿ񴥷���ִ���¼���

function viewCondition1() {
            document.getElementById("<%=PanelCondition1.ClientID%>").style.display = 'block';
        }
function viewCondition2() {
            document.getElementById("<%=PanelCondition2.ClientID%>").style.display = 'block';
        }
function viewCondition3() {
            document.getElementById("<%=PanelCondition3.ClientID%>").style.display = 'block';
        }
</script>
    <style type="text/css">
        .show
        {
            display: block;
        }
        .completionListElement
        {
            margin: 0px;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 150px !important;
            background-color: White;
            font-size: small;
        }
        .listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            color: windowtext;
            font-size: small;
        }
        .highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner" style="height:60px">
        <table width="100%">
            <tr>
                <td align="left" width="37%">
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radio_all" runat="server" Text="ȫ��" GroupName="shenhe" OnCheckedChanged="radio_CheckedChanged"
                                        AutoPostBack="True"/>
                    <asp:RadioButton ID="radio_symbol" runat="server" Text="���¼������ﵽ������Ա" GroupName="shenhe" OnCheckedChanged="radio_CheckedChanged"
                                        AutoPostBack="True" Checked="true" />
                   
                    
                    
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton_quanbu" runat="server" Text="ȫ��" GroupName="zhistate" OnCheckedChanged="radio_CheckedChanged"
                                        AutoPostBack="True"/>
                    <asp:RadioButton ID="RadioButton_zaizhi" runat="server" Text="��ְ" GroupName="zhistate" OnCheckedChanged="radio_CheckedChanged"
                                        AutoPostBack="True" Checked="true" />
                    <asp:RadioButton ID="RadioButton_lizhi" runat="server" Text="��ְ" GroupName="zhistate" OnCheckedChanged="radio_CheckedChanged"
                                        AutoPostBack="True" />
                </td>
                
                <td align="left">
                    <strong>���ţ�</strong>&nbsp;
                    <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    <strong>������</strong>
                    <asp:TextBox ID="txtName" runat="server" Width="80px"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtName"
                        ServicePath="~/OM_Data/OM_Data_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getdata" FirstRowSelected="true" CompletionListCssClass="completionListElement"
                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="highlightedListItem"
                        UseContextKey="false" EnableCaching="false">
                    </asp:AutoCompleteExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnNameQuery" Text="��ѯ" OnClick="btnNameQuery_OnClick" />
                    &nbsp;&nbsp;&nbsp;
                   <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />����ɸѡ</asp:HyperLink>
                     <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
                     </asp:PopupControlExtender>
                      <asp:Panel ID="palORG" Width="500px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server"> 
                         <table width="100%" >
                             <tr>       
                                 <td>
                                      <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                          <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="�ر�">X</a>
                                      </div>
                                      <br /><br />
                                 </td>
                             </tr>
                             <tr>
                                 <td>��ְʱ���</td>
                                 <td>
                                     <asp:TextBox ID="txt_rztimestart" runat="server"></asp:TextBox>
                                     <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtender2" runat="server" Format="yyyy.MM.dd" TargetControlID="txt_rztimestart">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td>��</td>
                                 <td>
                                     <asp:TextBox ID="txt_rztimeend" runat="server"></asp:TextBox>
                                     <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtender3" runat="server" Format="yyyy.MM.dd" TargetControlID="txt_rztimeend">
                                     </asp:CalendarExtender>
                                 </td>
                             </tr>
                             <tr>
                                 <td>��ְʱ���</td>
                                 <td>
                                     <asp:TextBox ID="txt_lztimestart" runat="server"></asp:TextBox>
                                     <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txt_lztimestart">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td>��</td>
                                 <td>
                                     <asp:TextBox ID="txt_lztimeend" runat="server"></asp:TextBox>
                                     <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd" TargetControlID="txt_lztimeend">
                                     </asp:CalendarExtender>
                                 </td>
                             </tr>
                             <tr>
                                 <td>������ٴ�</td>
                                 <td>
                                     <asp:TextBox ID="txt_kxmin" runat="server"></asp:TextBox>
                                 </td>
                                 <td>��</td>
                                 <td>
                                     <asp:TextBox ID="txt_kxmax" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>��ʹ����ٴ�</td>
                                 <td>
                                     <asp:TextBox ID="txt_ysymin" runat="server"></asp:TextBox>
                                 </td>
                                 <td>��</td>
                                 <td>
                                     <asp:TextBox ID="txt_ysymax" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>ʣ����ٴ�</td>
                                 <td>
                                     <asp:TextBox ID="txt_symin" runat="server"></asp:TextBox>
                                 </td>
                                 <td>��</td>
                                 <td>
                                     <asp:TextBox ID="txt_symax" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>��ͬ����</td>
                                 <td>
                                     <asp:TextBox ID="txt_htzt" runat="server"></asp:TextBox>
                                 </td>
                                 <td>��λ����</td>
                                 <td>
                                     <asp:TextBox ID="txt_gwxl" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4" align="center">
                                     <asp:Button ID="btn_confirm" runat="server" Text="ȷ��" UseSubmitBehavior="false"  OnClick="btn_confirm_Click"/>&nbsp;&nbsp; 
                                     <asp:Button ID="btn_clear" runat="server" Text="���" UseSubmitBehavior="false"   OnClick="btn_clear_Click"/>
                                 </td>
                             </tr>
                         </table>
                    </asp:Panel>  
                </td> 
            </tr>
            <tr>
                
                <td align="right" colspan="2">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnsave" Text="�������" OnClick="btnsave_OnClick" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnxglzsjclient" runat="server" Text="�޸���ְʱ��" OnClientClick="viewCondition2()" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch2" runat="server" TargetControlID="btnxglzsjclient"
                        PopupControlID="PanelCondition2" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="90" X="550">
                    </asp:ModalPopupExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnupdateinfo" Text="������Ա��Ϣ" OnClick="btnupdateinfo_OnClick" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnqlsjclient" runat="server" Text="����´�����ʱ��" OnClientClick="viewCondition1()" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch1" runat="server" TargetControlID="btnqlsjclient"
                        PopupControlID="PanelCondition1" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="90" X="775">
                    </asp:ModalPopupExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnholdel" Text="ʣ���������" OnClick="btnholdel_OnClick" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnupdateysy" Text="������ʹ�����" OnClick="btnupdateysy_OnClick" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_plexport" Text="����" OnClick="btn_plexport_OnClick" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_kouchunianjiaclient" Text="���¿۳��������" OnClientClick="viewCondition3()" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch3" runat="server" TargetControlID="btn_kouchunianjiaclient"
                        PopupControlID="PanelCondition3" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="90" X="850">
                    </asp:ModalPopupExtender>
                    &nbsp;&nbsp;&nbsp;
                </td>
             </tr>
        </table>
        
        <asp:Panel ID="PanelCondition1" runat="server" Width="250px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button runat="server" ID="btnnjqlsj" Text="ȷ��" OnClick="btnnjqlsj_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnclose1" runat="server" OnClick="btnclose1_Click" Text="ȡ��" />
                    </td>
                </tr>
                <tr>
                    <td>
                        �´�����ʱ�䣺<asp:TextBox ID="txtnjqlsj" Width="100px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtenderqlsjedit" runat="server" Format="yyyy-MM-dd" TargetControlID="txtnjqlsj">
                    </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelCondition2" runat="server" Width="250px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button runat="server" ID="btneditlztime" Text="ȷ��" OnClick="btneditlztime_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnclose2" runat="server" OnClick="btnclose2_Click" Text="ȡ��" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ��ְʱ�䣺<asp:TextBox ID="tblztime" runat="server"></asp:TextBox>
                    <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtenderlzsjedit" runat="server" Format="yyyy-MM-dd" TargetControlID="tblztime">
                    </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelCondition3" runat="server" Width="250px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button runat="server" ID="btn_kouchunianjia" Text="ȷ��" OnClick="btn_kouchunianjia_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnclose3" runat="server" OnClick="btnclose3_Click" Text="ȡ��" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ��ֹ�·ݣ�<asp:TextBox ID="tb_yearmonth" Width="100px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM��,yyyy��,dd��" TodaysDateFormat="yyyy��MM��dd��"
                            ID="CalendarExtender5" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                    </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                style="cursor: pointer">
                <asp:Repeater ID="rptHoliday" runat="server" OnItemDataBound="rptHoliday_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor" style="height: 40px; border: solid 1px">
                            <td width="55px" style="border: solid 1px">
                                <strong>���</strong>
                            </td>
                            <td width="70px" style="border: solid 1px">
                                <strong>����</strong>
                            </td>
                            <td width="90px" style="border: solid 1px">
                                <strong>����</strong>
                            </td>
                            <td width="130px" style="border: solid 1px">
                                <strong>����</strong>
                            </td>
                            
                            <td style="border: solid 1px">
                                <strong>��ͬ����</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>��λ����</strong>
                            </td>
                            
                            <td style="border: solid 1px">
                                <strong>��ְʱ��</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>��ְʱ��</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>������٣��죩</strong>
                            </td>
                            <td style="border: solid 1px" id="tzts" runat="server">
                                <strong>��������</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>��ʹ����٣��죩</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>ʣ����٣��죩</strong>
                            </td>
                            <td style="border: solid 1px" id="tdlook" runat="server">
                                <strong>������¼�鿴</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>�鿴���ʹ����ϸ</strong>
                            </td>
                             <td style="border: solid 1px">
                                <strong>�鿴����</strong>
                            </td>
                            <td>
                                <strong>��Ҫ�۳����������ԭ��</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                            <td>
                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(UCPaging1.PageSize))%>'></asp:Label>
                                <asp:CheckBox ID="cbxNumber" runat="server"/>
                                <asp:Label ID="lbNJ_ST_ID" runat="server" Visible="false" Text='<%#Eval("NJ_ST_ID")%>'></asp:Label>
                                <asp:Label ID="lbcheck" runat="server" Width="20px" Height="12px" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_NAME" runat="server" Text='<%#Eval("NJ_NAME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_WORKNUMBER" runat="server" Text='<%#Eval("NJ_WORKNUMBER") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_BUMEN" runat="server" Text='<%#Eval("NJ_BUMEN") %>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="ST_CONTR" runat="server" Text='<%#Eval("ST_CONTR") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ST_SEQUEN" runat="server" Text='<%#Eval("ST_SEQUEN") %>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="lbNJ_RUZHITIME" runat="server" Text='<%#Eval("NJ_RUZHITIME") %>'></asp:Label>
                            </td>
                            <td runat="server" id="tdlztime">
                                <asp:Label ID="lbST_LZSJ" runat="server" Text='<%#Eval("NJ_LIZHITIME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_KXTS" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td id="tdtzts" runat="server">
                                <asp:TextBox ID="tbNJ_TZTS" runat="server" Text='<%#Eval("NJ_TZTS") %>'></asp:TextBox>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_YSY" runat="server" Text='<%#Eval("NJ_YSY") %>'></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_LEIJI" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td id="lookjl" runat="server">
                                <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="������¼�鿴" NavigateUrl='<%#"~/OM_Data/OM_Person_Holtzjl.aspx?ID="+Eval("NJ_ST_ID") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    �鿴
                                </asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="HyperLink2" Target="_blank" ToolTip="�鿴��ϸ���ʹ����Ϣ" NavigateUrl='<%#"~/OM_Data/OM_KQlistsearch.aspx?flag=nianjia&stid="+Eval("NJ_ST_ID") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    �鿴��ϸ���ʹ����Ϣ
                               </asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="HyperLink3" Target="_blank" ToolTip="�鿴��ϸ���ʹ����Ϣ" NavigateUrl='<%#"~/OM_Data/OM_KQTJ.aspx?&stidnj="+Eval("NJ_ST_ID") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    �鿴
                               </asp:HyperLink>
                            </td>
                            <td align="center" style="width:120px">
                                <asp:TextBox ID="tbkcreason" runat="server" Text='<%#Eval("NJ_TYPE") %>' Width="100px" ToolTip='<%#Eval("NJ_TYPE") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
            <asp:Panel ID="palNoData" runat="server" HorizontalAlign="Center" ForeColor="Red">
                û�м�¼!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
