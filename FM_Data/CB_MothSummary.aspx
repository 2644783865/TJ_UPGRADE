<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="CB_MothSummary.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CB_MothSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    ����ͳ��
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
  function SubmitConfirm()
  {
    var txt=document.getElementById("<%=btnCurrentMonth.ClientID %>").value;
    if(txt=="��ӱ���ͳ��")
    {
      return confirm("ȷ�϶Ա��������ƺŽ��гɱ�ͳ����");
    }
    else if(txt=="���±��²��Ϸ�")
    {
      return confirm("ȷ�ϸ��±��������ƺųɱ���\r\r��ʾ��ֻ���²��Ϸѣ�");
    }
  }
  
   function Kqjcx(startyearmonth,endyearmonth)
   {   
    window.open("CB_MonthSummary_Detail1.aspx?startyearmonth="+startyearmonth+"&endyearmonth="+endyearmonth); 
   }
   
    function FYFTGZRG(year,month)
   {   
    var result=showModalDialog('CB_FYFT.aspx?year='+year+'&month='+month ,'subpage','dialogWidth:800px;dialogHeight:400px;center:yes;help:no;resizable:no;status:no'); //��ģ̬�Ӵ���,����ȡ����ֵ
    window.location.href = window.location.href;
   }
   function FYFTZZ(year,month)
   {   
    var result=showModalDialog('CB_FYFT_ZZFY.aspx?year='+year+'&month='+month ,'subpage','dialogWidth:800px;dialogHeight:400px;center:yes;help:no;resizable:no;status:no'); //��ģ̬�Ӵ���,����ȡ����ֵ
    window.location.href = window.location.href;
   }
 
    </script>

    <script language="javascript" type="text/javascript">
    
    function viewCondition2()
    {
     document.getElementById("<%=PanelCondition2.ClientID%>").style.display='block';//���ٵ�ҳ��ռ�
    }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>

    <script type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 2,
                fixedCols: 4,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       $(function() {
            sTable();
        });
    </script>

    <div class="box-wrapper" style="width: 100%;">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 17%;">
                            ʱ�䣺
                            <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;��&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;��&nbsp;
                        </td>
                        <td style="width: 50%;">
                            ����ţ�<asp:TextBox ID="txtrwh" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                            &nbsp;&nbsp; ��ͬ�ţ�<asp:TextBox ID="txthth" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                            &nbsp;&nbsp;��Ŀ���ƣ�<asp:TextBox ID="txtxmmc" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCx" OnClick="btnCx_OnClick" runat="server" Text="��ѯ"></asp:Button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnKqjcx" runat="server" Text="���ڼ��ѯ" OnClientClick="viewCondition2()"
                                OnClick="btnky_Click" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnKqjcx"
                                PopupControlID="UpdatePanel3" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="30">
                            </asp:ModalPopupExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExport" OnClick="btnExport_Click" runat="server" Text="����"></asp:Button>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition2" runat="server" Width="400px" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table width="400px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td align="left">
                                        &nbsp;��ʼʱ�䣺
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlstartcx1" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;��&nbsp;
                                        <asp:DropDownList ID="ddlstartcx2" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;��&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;����ʱ�䣺
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlendcx1" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;��&nbsp;
                                        <asp:DropDownList ID="ddlendcx2" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;��&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <br />
                                        <asp:Button ID="btnQd" runat="server" Text="ȷ ��" OnClick="btnkqck_OnClick" />
                                        <asp:Button ID="btnQx" runat="server" Text="ȡ ��" OnClick="btnback_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
            <div>
                <table width="98%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnFTrggz" runat="server" Text="ֱ���˹��ѷ�̯" OnClick="btnFT_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnzzfy" runat="server" Text="������÷�̯" OnClick="btnFTZZ_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hplcnfb" runat="server" NavigateUrl="~/FM_Data/FM_CNFB.aspx" Target="_blank"
                                Font-Underline="false" Visible="false">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" Visible="false" />�ְ�</asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/FM_Data/FM_FJCBIMPORT.aspx"
                                Target="_blank" Font-Underline="false" Visible="false">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" Visible="false" />�ֽ��ɱ����ݵ���</asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCurrentMonth" OnClick="btnCurrentMonth_onClick" OnClientClick="javascript:return SubmitConfirm();"
                                runat="server" BorderStyle="Solid" Text="����" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="box-outer">
            <div style="height: 405px; overflow: auto; width: 100%">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center">
                                <th rowspan="2">
                                    ���
                                </th>
                                <th rowspan="2">
                                    �����
                                </th>
                                <th rowspan="2">
                                    ��ͬ��
                                </th>
                                <th rowspan="2">
                                    ��Ŀ����
                                </th>
                                <th rowspan="2">
                                    ���·�
                                </th>
                                <%--<th rowspan="2">
                                ����
                            </th>--%>
                                <th colspan="3">
                                    ֱ���˹���
                                </th>
                                <th colspan="10">
                                    ֱ�Ӳ��Ϸ�
                                </th>
                                <th colspan="3">
                                    �������
                                </th>
                                <th rowspan="2">
                                    ��Э����
                                </th>
                                <th rowspan="2">
                                    ���ڷְ�
                                </th>
                                <th rowspan="2">
                                    �˷�
                                </th>
                                <th rowspan="2">
                                    �ֽ��ɱ�(����)
                                </th>
                                <th rowspan="2">
                                    ����
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    ���ӷ���
                                </th>
                                <th>
                                    ���ڽṹ����
                                </th>
                                <th>
                                    С��
                                </th>
                                <th>
                                    �⹺��
                                </th>
                                <th>
                                    ��ɫ����
                                </th>
                                <th>
                                    ������
                                </th>
                                <th>
                                    ����
                                </th>
                                <th>
                                    �ͼ�
                                </th>
                                <th>
                                    ���
                                </th>
                                <th>
                                    ��׼��
                                </th>
                                <th>
                                    ����Ϳ��
                                </th>
                                <th>
                                    ������
                                </th>
                                <th>
                                    ����С��
                                </th>
                                <th>
                                    �̶��������
                                </th>
                                <th>
                                    �ɱ��������
                                </th>
                                <th>
                                    �������С��
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="row" class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>
                                    <asp:CheckBox ID="cbxSelect" runat="server" Visible="true" />
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("PMS_TSAID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhth" runat="server" Text='<%#Eval("TSA_PJID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxmmc" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbnyf" runat="server" Text='<%#Eval("AYTJ_YEARMONTH")%>'></asp:Label>
                                </td>
                                <%--<td align="center">
                                <asp:TextBox ID="tbgz" runat="server" Width="80px" Text='<%#Eval("AYTJ_GZ")%>' BorderStyle="None"
                                    BackColor="Transparent"></asp:TextBox>
                            </td>--%>
                                <td align="center">
                                    <asp:TextBox ID="tbjjfy" runat="server" Width="80px" Text='<%#Eval("AYTJ_JJFY")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbjgyzfy" runat="server" Width="80px" Text='<%#Eval("AYTJ_JGYZFY")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbzjrgxj" runat="server" Width="80px" Text='<%#Eval("AYTJ_ZJRGFXJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbwgj" runat="server" Width="80px" align="center" Text='<%#Eval("WGJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbhsjs" runat="server" Width="80px" align="center" Text='<%#Eval("HSJS")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbhcl" runat="server" Width="80px" align="center" Text='<%#Eval("HCL")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbzj" runat="server" Width="80px" align="center" Text='<%#Eval("ZJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbdj" runat="server" Width="80px" align="center" Text='<%#Eval("DJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbzc" runat="server" Width="80px" align="center" Text='<%#Eval("ZC")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbbzj" runat="server" Width="80px" align="center" Text='<%#Eval("BZJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyqtl" runat="server" Width="80px" align="center" Text='<%#Eval("YQTL")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbqtcl" runat="server" Width="80px" align="center" Text='<%#Eval("QTCL")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbclxj" runat="server" Width="80px" align="center" Text='<%#Eval("CLXJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbgdzzfy" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_GDZZFY")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbkbzzfy" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_KBZZFY")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbzzfyxj" runat="server" Width="80px" align="center" Text='<%#Eval("ZZFYXJ")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbwxfy" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_WXFY")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbcnfb" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_CNFB")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyf" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_YF")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbfjcb" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_FJCB")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbqt" runat="server" Width="80px" align="center" Text='<%#Eval("AYTJ_QT")%>'
                                        BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th colspan="5" align="right">
                                    �ϼƣ�
                                </th>
                                <%--<th align="center">
                                <asp:Label ID="lbgzhj" runat="server"></asp:Label>
                            </th>--%>
                                <th align="center">
                                    <asp:Label ID="lbjjfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbjgyzfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbzjrghj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbwgjhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbhsjshj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbhclhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbzjhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbdjhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbzchj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbbzjhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbyqtlhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbqtclhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbclhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbgdzzfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbkbzzfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbzzfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbwxfyhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbcnfbhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbyfhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbfjcbhj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                    <asp:Label ID="lbqthj" runat="server"></asp:Label>
                                </th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    û�м�¼!<br />
                    <br />
                </asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
