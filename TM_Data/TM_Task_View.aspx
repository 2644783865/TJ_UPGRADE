<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Task_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Task_View" %>

<%@ Register Src="../Controls/UCPagingOfMS.ascx" TagName="UCPagingOfMS" TagPrefix="uc2" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="labprostru" runat="server" Text=""></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/jquery/jquery-1.11.0.js" type="text/javascript"></script>

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/wxBOM.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">

        var dbl_click = false;

        function openLink(url) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Original_Data_Operate.aspx?NoUse=" + time + "&register=" + url, '', "dialogHeight:700px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

        function NodeCheck() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") //���treeview��checkbox�Ǵ���  
            {
                var d = o.id; //��õ�ǰcheckbox��id;
                var e = d.replace("CheckBox", "Nodes"); //ͨ���鿴�ű���Ϣ,��ð��������ӽڵ�div��id  
                var div = window.document.getElementById(e); //���div����  
                if (div != null)  //�����Ϊ�����ʾ,�����Խڵ�  
                {
                    var check = div.getElementsByTagName("INPUT"); //���div�����е���input��ʼ�ı��  
                    for (i = 0; i < check.length; i++) {
                        if (check[i].type == "checkbox") //�����checkbox  
                        {
                            check[i].checked = o.checked; //�ֽڵ��״̬�͸��ڵ��״̬��ͬ,���ﵽȫѡ  
                        }
                    }
                }

                //���ӽڵ��ʱ��,ʹ���ڵ��״̬�ı�,����Ϊȫѡ
                var divid = o.parentElement.parentElement.parentElement.parentElement.parentElement; //�ӽڵ����ڵ�div      
                var id = divid.id.replace("Nodes", "CheckBox"); //��ø��ڵ��id
                var div_father = window.document.getElementById(id);
                while (div_father != null) {
                    var checkbox = divid.getElementsByTagName("INPUT"); //��ȡ�����ӽڵ���
                    var s = 0;
                    for (i = 0; i < checkbox.length; i++) {
                        if (checkbox[i].checked)  //�ж��ж����ӽڵ㱻ѡ��  
                        {
                            s++;
                        }
                    }
                    if (s == checkbox.length)  //���ȫ��ѡ�� ���� ѡ���������һ�����ڵ���ӽڵ� ��  
                    {                               //    ��ʼ�ĸ��ڵ��״̬��ȻΪѡ��״̬  
                        window.document.getElementById(id).checked = true;
                    }
                    else {                               //����Ϊûѡ��״̬  
                        window.document.getElementById(id).checked = false;
                    }
                    divid = window.document.getElementById(id).parentElement.parentElement.parentElement.parentElement.parentElement; //�ӽڵ����ڵ�div     
                    id = divid.id.replace("Nodes", "CheckBox"); //��ø��ڵ��id
                    div_father = window.document.getElementById(id);
                }
            }
        }


        //�²���ϼƻ�
        function MP_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Mp_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //�²���ϸ
        function MS_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Ms_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid_table=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //�²���Э�ƻ�
        function OUT_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Out_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //�²����
        function MarReplace_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_MarReplaceView.aspx?NoUse=" + time + "&xuhao_engid_marid_table=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

        function showPanel11() {
            var obj = document.getElementById("<%=Panel11.ClientID %>");
            if (obj.style.display == "none") {
                obj.style.display = "block";
            }
            else {
                obj.style.display = "none"
            }
        }

        function CheckXishu(obj) {
            var pattem = /^\d+(\.\d+)?$/; //������֤
            if (!pattem.test(obj.value)) {
                alert("��������ȷ�ļƻ�ϵ��������");
                obj.value = "1";
            }
            else if (parseFloat(obj.value) < 1) {
                alert("�ƻ�ϵ������С��1������");
                obj.value = "1";
            }
            else if (parseFloat(obj.value) > 2) {
                var con = confirm("�趨��ϵ�����ܹ��󣡣���\r\r��ȷ�Ͻ�ϵ���趨Ϊ��" + obj.value + "����?");
                if (con == false) {
                    obj.value = "1";
                }
            }
        }
        function EditData(id, zongxu) {
        var myDate = new Date();
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=edit&time="+myDate.toLocaleString());
        }
        function ChangeData(id, zongxu) {
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=change");
        }
        function ChangeOneData(id, zongxu) {
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=changeOne");
        }


        //����¼�Ƿ�ѡ

        function CheckSelected() {
            var table = document.getElementById("<%=GridView2.ClientID%>");

            if (table != null) {
                var tr = table.getElementsByTagName("tr");
                var checkbox;
                var bmid;
                var index = 0;

                for (i = 1; i < tr.length; i++) {
                    checkbox = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if (checkbox.checked) {
                        index++;
                    }
                }

                if (index == 0) {
                    alert("�빴ѡҪ��������ļ�¼������");
                    return false;
                }
            }
            else {
                alert("û�пɲ�����¼������");
                return false;
            }
            return true;
        }


        $(function() {
            $("#<%=GridView1.ClientID %> tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");

                $(this).toggleClass("techBackColor");
            });
        });
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="98%">
                            <tr>
                                <td style="width: 24%">
                                    �����:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                    <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    ��ͬ��:<asp:Label ID="lab_hetongNum" runat="server"></asp:Label>
                                    <input id="hetong_num" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    ��Ŀ����:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                    <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="ԭʼ���ݲ鿴" TabIndex="0">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExport" />
                            </Triggers>
                            <ContentTemplate>
                                <div>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                        <tr>
                                            <td align="center">
                                                ȫѡ/ȡ��<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
                                                <asp:Button ID="btnSelect_org" ToolTip="ѡ����ֹ�������ѡ,��ֹ������м�¼ѡ��" runat="server" OnClick="btnSelect_org_OnClick"
                                                    Text="��ѡ" />
                                            </td>
                                            <td align="left">
                                                <asp:HyperLink ID="hylMore" CssClass="hand" onclick="showPanel11();" runat="server">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />�������</asp:HyperLink>
                                            </td>
                                            <td align="right">
                                                ��ѯ���:<asp:DropDownList ID="ddlQueryType" runat="server">
                                                    <asp:ListItem Text="-��ѡ��-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="����" Value="BM_ZONGXU"></asp:ListItem>
                                                    <asp:ListItem Text="��������" Value="BM_CHANAME"></asp:ListItem>
                                                    <asp:ListItem Text="ͼ��" Value="BM_TUHAO"></asp:ListItem>
                                                    <asp:ListItem Text="���" Value="BM_GUIGE"></asp:ListItem>
                                                    <asp:ListItem Text="���ϱ���" Value="BM_MARID"></asp:ListItem>
                                                    <asp:ListItem Text="��������" Value="BM_MANAME"></asp:ListItem>
                                                    <asp:ListItem Text="���Ϲ��" Value="BM_MAGUIGE"></asp:ListItem>
                                                    <asp:ListItem Text="����(��׼)" Value="BM_STANDARD"></asp:ListItem>
                                                    <asp:ListItem Text="����" Value="BM_MAQUALITY"></asp:ListItem>
                                                    <asp:ListItem Text="��ע" Value="BM_NOTE"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMCorZX" runat="server"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnOrgQuery" runat="server" Width="40" Text="��ѯ" OnClick="btnOrgQuery_OnClick" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnClear" runat="server" Width="40" Text="����" OnClick="btnClear_OnClick" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnExport" runat="server" Width="40" Text="����" OnClick="btnExport_OnClick" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_OnClick">
                                                    <asp:Image ID="Image11" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />ԭʼ�����޸�</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_OnClick">
                                                    <asp:Image ID="Image12" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />ԭʼ���ݱ��</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_OnClick">
                                                    <asp:Image ID="Image10" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />�������</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_OnClick" OnClientClick="return confirm('��ȷ��Ҫȡ��������ϸ��');">
                                                    <asp:Image ID="Image7" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />���ȡ��</asp:LinkButton>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />����ɸѡ</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-1000" OffsetY="-55" TargetControlID="HyperLink1"
                                                    PopupControlID="palORG">
                                                </cc1:PopupControlExtender>
                                                <%--<cc1:ModalPopupExtender ID="ModalPopupExtenderSearch"  runat="server" Enabled="true"  TargetControlID="btnSelectOrg" PopupControlID="UpdatePanel2"  Drag="false"   Y="30" >
          </cc1:ModalPopupExtender>--%>
                                                <asp:Panel ID="palORG" Width="90%" Style="display: none; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                                title="�ر�">X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ��������:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlpartsname" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Button ID="btn_OrgQuery" runat="server" UseSubmitBehavior="false" OnClick="btnOrgQuery_OnClick"
                                                                            Text="�� ѯ" />&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnOrgClear" runat="server" UseSubmitBehavior="false" OnClick="btnOrgClear_OnClick"
                                                                            Text="�� ��" />&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ��������:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlmatername" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td rowspan="9">
                                                                        <uc3:UserDefinedQueryConditions ID="udqOrg" runat="server" QueryColumnsType="TaskView"
                                                                            QueryRows="8" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ���Ϲ��:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlguige" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ���ϲ���:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlcaizhi" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ������ϸ:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgInMS" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="��" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="��" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        ��������:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgShape" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        �Ƿ񶨳�:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgFix" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="��" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="��" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ������ϸ:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgMSState" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="δ����" Value="WSC"></asp:ListItem>
                                                                            <asp:ListItem Text="�����" Value="SSZ"></asp:ListItem>
                                                                            <asp:ListItem Text="ͨ��" Value="TG"></asp:ListItem>
                                                                            <asp:ListItem Text="����" Value="BH"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        ���ϼƻ�:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgMPState" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="δ����" Value="WSC"></asp:ListItem>
                                                                            <asp:ListItem Text="δ�ύ" Value="WTJ"></asp:ListItem>
                                                                            <asp:ListItem Text="�����" Value="SSZ"></asp:ListItem>
                                                                            <asp:ListItem Text="ͨ��" Value="TG"></asp:ListItem>
                                                                            <asp:ListItem Text="����" Value="BH"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ����ϵ��:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlXishu" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                                                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="(1,1.05]" Value="1.05"></asp:ListItem>
                                                                            <asp:ListItem Text="[1.05,1.1]" Value="1.051.01"></asp:ListItem>
                                                                            <asp:ListItem Text=">1.1" Value="1.1"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        ��ʾ����:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgJishu" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="1��" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="2��" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="3��" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="4��" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="5��" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="6��" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="7��" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="8��" Value="7"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        ���üƻ�:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlWMarPlan" runat="server">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="�ύ" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="���ύ" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:Panel ID="Panel11" runat="server" Style="display: none;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnDelete" Width="40" runat="server" Text="ɾ��" OnClientClick="return chksure()"
                                                                    OnClick="btnDelete_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnWmar" Width="55" runat="server" Text="�ƻ�Y/N" OnClientClick="return chkwmar()"
                                                                    OnClick="btnWmar_Click" />
                                                            </td>
                                                            <%--<td align="right">
                                                                �滻/׷��:
                                                            </td>
                                                            <td style="width: 150px;">
                                                                <asp:DropDownList ID="ddlRepType" runat="server">
                                                                    <asp:ListItem Text="ͼ��" Value="BM_TUHAO" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="��ע" Value="BM_NOTE"></asp:ListItem>
                                                                    <asp:ListItem Text="��������" Value="BM_MASHAPE"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlReplaceType" runat="server">
                                                                    <asp:ListItem Text="�滻" Value="Replace" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="׷��" Value="Add"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtStart" runat="server"></asp:TextBox>>><asp:TextBox ID="txtEnd"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" Width="60" OnClick="btnSave_Replace" OnClientClick="return chkreplace();"
                                                                    Text="�滻/׷��" />
                                                            </td>--%>
                                                            <td style="width: 20px">
                                                                <asp:Button ID="btnCalWeight" Width="55" runat="server" Text="��������" OnClick="btnCalWeight_Click" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                ͼֽ���أ�
                                                            </td>
                                                            <td style="width: 30px">
                                                                <input type="text" id="txtTotalTu" runat="server" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                �������أ�
                                                            </td>
                                                            <td>
                                                                <input id="txtTotalMP" type="text" runat="server" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                <asp:DropDownList runat="server" ID="ddlChangeItem">
                                                                    <asp:ListItem Text="ͼ��" Value="BM_TUHAO" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20px">
                                                                <input id="txtEquipMap" type="text" runat="server" />
                                                            </td>
                                                            <td style="width: 15px">
                                                                ��Ϊ
                                                            </td>
                                                            <td style="width: 20px">
                                                                <input id="txtChangeMap" type="text" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnChangeMap" OnClick="btnChangeMap_Click" Text="�滻" />
                                                            </td>
                                                            <td>
                                                                <input id="Button4" type="button" style="width: 40px" onclick="showPanel11();" value="����" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal"
                            RepeatColumns="17" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"
                            AutoPostBack="true" RepeatLayout="Table">
                        </asp:CheckBoxList>
                        <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            û�м�¼!</asp:Panel>
                        <div id="divGvData" runat="server" style="position: relative; top: 0px; left: 0px;
                            overflow: scroll; width: 100%; height: 480px;" onscroll="funGrilViewScroll(); return false;">
                            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" Style="white-space: normal"
                                runat="server" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333"
                                DataKeyNames="BM_XUHAO" OnRowDataBound="GridView1_RowDataBound" AllowPaging="False">
                                <RowStyle BackColor="#EFF3FB" />
                                <HeaderStyle />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete();setMap(this)"
                                                CssClass="checkBoxCss" />
                                            <input id="Hidden1" type="hidden" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�к�" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                            <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString()+"-"+Eval("BM_ISMANU").ToString() %>' />
                                            <input type="hidden" runat="server" value='<%# Eval("BM_ID") %>' id="hidBmId" />
                                            <%--���ϼƻ�״̬�����ϱ��״̬����ϸ״̬����ϸ���״̬�����ϱ��롢�Ƿ�����ϼƻ�--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="BM_TUHAO" HeaderText="ͼ��(��ʶ��)" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MARID" HeaderText="���ϱ���" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_ZONGXU" HeaderText="����" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_CHANAME" HeaderText="��������" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAGUIGE" HeaderText="���Ϲ��" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAQUALITY" HeaderText="����" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMBER" HeaderText="��̨����|������|�ƻ�����" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TUUNITWGHT" DataFormatString="{0:F2}" HeaderText="ͼֽ����(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TUTOTALWGHT" DataFormatString="{0:F2}" HeaderText="ͼֽ����(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MASHAPE" HeaderText="��������" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TECHUNIT" HeaderText="������λ" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_YONGLIANG" HeaderText="��������" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="����(m2)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="�ƻ�����(m2)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="�����ܳ�(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="���ϵ���(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="��������(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="����(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="���(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="���ϱ�ע" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px">
                                        <ItemTemplate>
                                            <div style="width: 100px">
                                                <label>
                                                    <%# Eval("BM_NOTE")%></label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_XIALIAO" HeaderText="���Ϸ�ʽ" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="��������" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcess" runat="server" Text='<%#Eval("BM_PROCESS") %>' Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_KU" HeaderText="��" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="��ע" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div style="width: 100px">
                                                <label>
                                                    <%# Eval("BM_ALLBEIZHU")%></label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="��������"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_STANDARD" HeaderText="����" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_ISMANU" HeaderText="������ϸ" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="�Ƿ񶨳�" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_WMARPLAN" HeaderText="���ϼƻ�" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                        <uc1:UCPaging ID="UCPagingOrg" runat="server" />
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="ԭʼ����>>���ϼƻ�" TabIndex="1">
                    <ContentTemplate>
                        <div>
                            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                <tr>
                                    <td align="center">
                                        ȫѡ/ȡ��<input id="ckbSelectAll_mp" runat="server" onclick="selectAll(this);" type="checkbox" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSelect_mp" ToolTip="ѡ����ֹ�������ѡ,��ֹ������м�¼ѡ��" runat="server" OnClick="btnSelect_mp_OnClick"
                                            Text="��ѡ" />
                                    </td>
                                    <td align="right" class="notbrk">
                                        ���ϼƻ����:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmptype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlnamewithcheckboxlist_SelectedIndexChanged">
                                            <asp:ListItem Text="ȫ���ύ" Value="��������"></asp:ListItem>
                                            <asp:ListItem Text="�ֲ�" Value="�ֲ�"></asp:ListItem>
                                            <asp:ListItem Text="��׼��" Value="��"></asp:ListItem>
                                            <asp:ListItem Text="�ɹ���Ʒ" Value="�ɹ���Ʒ"></asp:ListItem>
                                            <asp:ListItem Text="�ǽ���" Value="��"></asp:ListItem>
                                            <asp:ListItem Text="�ͼ�" Value="�ͼ�"></asp:ListItem>
                                            <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                    <td align="center">
                                        <asp:HyperLink ID="HyperLink4" CssClass="hand" runat="server">
                                            <asp:Image ID="Image3" ImageUrl="~/Assets/icons/forum.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />�ύ����</asp:HyperLink>
                                        <cc1:PopupControlExtender ID="PopupControlExtender4" CacheDynamicResults="false"
                                            Position="Left" Enabled="true" runat="server" OffsetX="-50" OffsetY="-100" TargetControlID="HyperLink4"
                                            PopupControlID="Panel3">
                                        </cc1:PopupControlExtender>
                                        <asp:Panel ID="Panel3" Style="display: none; width: 300px; visibility: hidden; border-style: solid;
                                            border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" style="margin-left: -350px">
                                                        <tr>
                                                            <td>
                                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                    <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                        title="�ر�">X</a>
                                                                </div>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="notbrk">
                                                                ͼ�ţ�
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList runat="server" ID="ddlShebei" Width="70px">
                                                                </asp:DropDownList>
                                                                <span style="color: Red">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                �ύ���:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlChange" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlnamewithcheckboxlist_SelectedIndexChanged">
                                                                    <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="���" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="ȡ��" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span style="color: Red">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="tr5">
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TreeView ID="TreeMp" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                    SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                    ShowLines="true" NodeIndent="20" ToolTip="���-����-����" onclick="NodeCheck();">
                                                                </asp:TreeView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td align="center">
                                        <asp:HyperLink ID="HyperLink2" CssClass="hand" runat="server">
                                            <asp:Image ID="Image5" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />����ɸѡ</asp:HyperLink>
                                        <cc1:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                            Position="Left" Enabled="true" runat="server" OffsetX="-900" OffsetY="-55" TargetControlID="HyperLink2"
                                            PopupControlID="palMp">
                                        </cc1:PopupControlExtender>
                                        <asp:Panel ID="palMp" Width="80%" Style="display: none; visibility: hidden; border-style: solid;
                                            border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnMPQuery" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                    <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                        title="�ر�">X</a>
                                                                </div>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                ��������:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlbjname" runat="server" Width="200px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" class="notbrk">
                                                                ��������:
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:DropDownList ID="ddlname" runat="server" Width="200px">
                                                                    <asp:ListItem Text="-��ѡ��-" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                ��������:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlmpxz" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="right">
                                                                <hr />
                                                                <asp:Button ID="btnMPQuery" runat="server" UseSubmitBehavior="false" OnClick="ddlname_SelectedIndexChanged"
                                                                    Text="�� ѯ" />
                                                                &nbsp;&nbsp;<asp:Button ID="btnMPClear" runat="server" UseSubmitBehavior="false"
                                                                    OnClick="btnMPClear_OnClick" Text="�� ��" />&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="left" style="width: 98%">
                                                                <uc3:UserDefinedQueryConditions runat="server" ID="udqcMp" QueryColumnsType="TaskView"
                                                                    QueryRows="8" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                ��ʾ����:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlJishuMp" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlJishuMp_OnSelectedIndexChanged">
                                                                    <asp:ListItem Text="1��" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2��" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3��" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4��" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5��" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6��" Value="6"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="left">
                                                                <asp:TreeView ID="TreeViewMp" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                    SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                    ShowLines="true" NodeIndent="20" ToolTip="���-����-����" onclick="NodeCheck();">
                                                                </asp:TreeView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="6">
                                                                ��������:
                                                            </td>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <asp:CheckBoxList ID="cblMarid" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                                        RepeatLayout="Table">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td align="right">
                                        <asp:CheckBox ID="ckbMpMore" runat="server" ToolTip="�ƻ���������100����һ����ȫ�����ɼƻ�" OnCheckedChanged="ckbMpMore_OnCheckedChanged"
                                            AutoPostBack="true" />����100�� &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="mpsubmit" runat="server" Text="���ɲ��ϼƻ�" OnClientClick="return mpId(this)"
                                            OnClick="mpsubmit_Click" />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkiffast" runat="server" />�Ƿ�Ӽ�����
                                        <asp:Button ID="mpChange" runat="server" Text="���ϼƻ����" OnClientClick="return mpId(this)"
                                            OnClick="mpChange_Click" />
                                        <asp:Button ID="mpCutDown" runat="server" Text="ȡ�����ϼƻ�" OnClientClick="return mpId(this)"
                                            OnClick="mpCutDown_Click" />
                                        <asp:LinkButton ID="lkbtnToMpPage" runat="server" OnClick="lkbtnToMpPage_OnClick"
                                            Visible="false">
                                            <asp:Image ID="Image9" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />ת���ƻ�����ҳ��</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            û�м�¼!</asp:Panel>
                        <asp:GridView ID="GridView2" OnRowDataBound="GridView2_OnRowDataBound" Width="100%"
                            CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            ForeColor="#333333">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�к�" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfMPtate" Value='<%#Eval("BM_MPSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMPChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="BM_XUHAO" HeaderText="���"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="ͼ��(��ʶ��)" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MARID" HeaderText="���ϱ���" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="���Ϲ��" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="�ƻ�����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="��λ" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="���ϵ���(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="��������(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="����(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="���(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="�ƻ�����(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="�����ܳ�(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="��������"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="BM_PROCESS"    HeaderText="��������"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"    HeaderText="��"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:BoundField HeaderText="�Ƿ񶨳�" HeaderStyle-Wrap="false" DataField="BM_FIXEDSIZE"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="����">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <%--  <asp:BoundField HeaderText="�ؼ�����"    HeaderStyle-Wrap="false" DataField="BM_KEYCOMS" ItemStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField HeaderText="���״̬" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MPSTATUS").ToString()=="1"?"ɾ��":Eval("BM_MPSTATUS").ToString()=="2"?"����":Eval("BM_MPSTATUS").ToString()=="3"?"�޸�":"����" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="BM_GUIGE"    HeaderText="���"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="��ע" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllbeizhu" CssClass="notbrk" runat="server" Text='<%#Eval("BM_ALLBEIZHU") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField HeaderText="��ע" DataField="BM_ALLBEIZHU" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />--%>
                                <asp:BoundField HeaderText="��" DataField="BM_KU" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField HeaderText="����" DataField="BM_OLDINDEX" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" Visible="false" />
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <uc1:UCPaging ID="UCPagingMP" runat="server" />
                        <asp:Panel ID="Panel1" runat="server" Font-Size="Large" BackColor="#B3CDE8">
                            ��ע����ɫ��־Ϊ����޸Ĺ��Ĳ���!��ɫ��־Ϊ©��Ĳ���!��ɫ��־Ϊ��ɾ���Ĳ���!<br />
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="ԭʼ����>>������ϸ" TabIndex="2">
                    <ContentTemplate>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel123" runat="server">
                                <Triggers>
                                    <%--    <asp:PostBackTrigger ControlID="btnExportMsNew" />--%>
                                </Triggers>
                                <ContentTemplate>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                        <tr>
                                            <td align="right">
                                                �������ƣ�
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddpMSbjName" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                �Ƿ�����:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMSinMS" runat="server">
                                                    <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                    <asp:ListItem Text="��" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="��" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnExportMs" runat="server" Text="������ϸ" OnClick="btnExportMs_OnClick"
                                                    OnClientClick="return confirm('�����Ǵ�ԭʼ�����е�����������ϸ��ȷ�ϵ�����\r\r��ʾ:����ѡ��Ĳ�ѯ��������������');" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Text="�� ѯ" OnClick="ddpMSbjName_OnSelectedIndexChanged" />
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink3" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image6" ImageUrl="~/Assets/icons/forum.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />�ύ����</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-360" OffsetY="-55" TargetControlID="HyperLink3"
                                                    PopupControlID="palMs">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="palMs" Style="display: none; width: 750px; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%" style="margin-left: -360px">
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; text-align: center; position: absolute; top: 8px;
                                                                            right: 100px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-decoration: none; padding: 5px;" title="�ر�">
                                                                                X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 10%;" class="notbrk">
                                                                        ��ϸ״̬:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlMSChange" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddpMSbjName_OnSelectedIndexChanged">
                                                                            <asp:ListItem Text="-��ѡ��-" Value="-��ѡ��-"></asp:ListItem>
                                                                            <asp:ListItem Text="���" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="color: Red">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_1">
                                                                    <td align="right" class="notbrk">
                                                                        �ύ��ʽ:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlAdjustPart" runat="server">
                                                                            <asp:ListItem Text="�Զ����ύ��" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="ȫ���ύ" Value="1"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_4">
                                                                    <td>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TreeView ID="TreeView1" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                            SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                            ShowLines="true" NodeIndent="20" ToolTip="����-����" onclick="NodeCheck();">
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_5" visible="false">
                                                                    <td>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TreeView ID="TreeViewMsChange" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                            SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                            ShowLines="true" NodeIndent="20" ToolTip="����-����" onclick="NodeCheck();">
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />����ɸѡ</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender5" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-300" OffsetY="-55" TargetControlID="HyperLink5"
                                                    PopupControlID="palMS3">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="palMS3" Style="display: none; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                                title="�ر�">X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="ddpMSbjName_OnSelectedIndexChanged"
                                                                            Text="�� ѯ" />
                                                                        &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick"
                                                                            Text="�� ��" />&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 98%">
                                                                        <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskView"
                                                                            QueryRows="8" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Button ID="chSubmit" runat="server" Text="�����ϸ" Visible="false" OnClientClick="return confirm(&quot;ȷ�ϱ��������ϸ��\r\r��ʾ��\r\r�������м�¼������������ϸ�����&quot;);"
                                                    OnClick="chSubmit_Click" />
                                                <asp:Button ID="mssubmit" runat="server" Text="����������ϸ" Visible="false" OnClientClick="return confirm(&quot;ȷ������������ϸ��&quot;);"
                                                    OnClick="mssubmit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Panel ID="NoDataPanel3" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            û�м�¼!</asp:Panel>
                        <asp:GridView ID="GridView4" OnRowDataBound="GridView4_OnRowDataBound" runat="server"
                            AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
                            Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="�к�" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                        <asp:HiddenField ID="hdfMSState" Value='<%#Eval("BM_MSSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMSChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="ͼ��" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="���Ϲ��" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUMBER" HeaderText="��̨����|������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUUNITWGHT" HeaderText="ͼֽ����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUTOTALWGHT" HeaderText="ͼֽ����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="��λ" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="����" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="���" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_NOTE" HeaderText="��ע" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_XIALIAO" HeaderText="���Ϸ�ʽ" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PROCESS" HeaderText="��������" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="�����ܳ�(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="����(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="�ƻ�����(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="��ע" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="���״̬" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"ɾ��":Eval("BM_MSSTATUS").ToString()=="2"?"����":Eval("BM_MSSTATUS").ToString()=="3"?"�޸�":"����" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <uc1:UCPaging ID="UCPagingMS" runat="server" />
                        <asp:Panel ID="Panel2" runat="server" Font-Size="Large" BackColor="#B3CDE8">
                            ��ע����ɫ��־Ϊ����޸�!��ɫ��־Ϊ�������!��ɫ��־Ϊ���ɾ��!<br />
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <input id="deleteId" type="text" runat="server" value="0" readonly="readonly" style="display: none" />
            <input id="mpid" type="text" runat="server" readonly="readonly" style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 70%; right: 40%">
                <table>
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
</asp:Content>
