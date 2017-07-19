<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"  AutoEventWireup="true" CodeBehind="PM_Xie_IntoOrder.aspx.cs"  Inherits="ZCZJ_DPF.PM_Data.PM_Xie_IntoOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �༭
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
        <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
        <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/copy.js" type="text/javascript" charset="GB2312" language="javascript"></script>
     <script language="javascript" type="text/javascript">
         function document.onreadystatechange() {
             if (document.readyState == "complete") {
                 var ptc = "<%=gloabptc%>";
                 var col = 2;
                 othptcfind(ptc, col)
             }
         }
         function getClientId() {
             var paraId1 = '<%= LabelDate.ClientID %>';
             return { Id1: paraId1 };
         }
    </script>
 <div class="RightContent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        ��Э����
                                    </td>
                                    <td align="right">
                                    
                                    <asp:Button ID="btn_delete" runat="server" Text="ɾ��" ForeColor="Red" OnClick="btn_delete_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="confirm" runat="server" Text="����" OnClick="save_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_shangcha" runat="server" Text="�ϲ�" OnClick="btn_shangcha_Click"  />&nbsp;&nbsp;
                                        <asp:Button ID="btn_xiacha" runat="server" Text="�²�" OnClick="btn_xiacha_Click" />&nbsp;&nbsp;
                                      <%--  <asp:Button ID="btn_concel" runat="server" Text="ȡ��" OnClick="btn_concel_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                        <asp:Button ID="goback" runat="server" Text="����" OnClick="goback_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                 <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="4">
                                        ��Э����
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <span style="color: Red">ע�����������ֶ������һ�У��ɴ������������</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;�ţ�<asp:Label ID="LabelCode" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;�ڣ�<asp:Label ID="LabelDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20%;" align="left" >
                                    &nbsp;&nbsp;&nbsp;�Ƶ��ˣ�&nbsp;&nbsp;&nbsp;<asp:Label ID="lab_zdr" runat="server" ></asp:Label>
                                    </td>
                                    <td style="width: 40%;" align="left">
                                        &nbsp;&nbsp;&nbsp;��Ӧ�̣�<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 60%;" colspan="3">
                                        &nbsp;&nbsp;&nbsp;������ע��
                                        <asp:TextBox ID="Tb_note" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                 <div style="border: 1px solid #000000; height: 360px;">
                            <div class="cpbox7 xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="PurorderdetailRepeater" runat="server"  >
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>�к�</strong>
                                                </td>
                                                <td>
                                                <strong>�ƻ����ٺ�</strong>
                                                </td>
                                                <td>
                                                    <strong>�ӹ�����</strong>
                                                </td>
                                                <td>
                                                    <strong>���㵥��</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>ͼ��</strong>
                                                </td>
                                                <td>
                                                    <strong>��Э����</strong>
                                                </td>
                                                <td>
                                                    <strong>��Э����</strong>
                                                </td>
                                                <td>
                                                    <strong>��������</strong>
                                                </td>
                                                  <td>
                                                    <strong>ʵ�ʽ�������</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>���</strong>
                                                </td>
                                                <td>
                                                    <strong>��ע</strong>
                                                </td>
                                                <td >
                                                    <strong>�ȼ۵���</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                <div style="width:200px;white-space:normal">
                                                    <asp:Label ID="TO_PTC" runat="server" Text='<%#Eval("TO_PTC")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_JGNUM" runat="server" Text='<%#Eval("PIC_JGNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_JSNUM" runat="server" Text='<%#Eval("PIC_JSNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_PTCODE" runat="server" Text='<%#Eval("PIC_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_MNAME" runat="server" Text='<%#Eval("PIC_MNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_WXTYPE" runat="server" Text='<%#Eval("PIC_WXTYPE")%>'></asp:Label>
                                                </td>
                                                  <td>
                                                    <asp:Label ID="TO_PROCESS" runat="server" Text='<%#Eval("TO_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                  <%--  <asp:TextBox ID="TO_JHQ" runat="server" Text='<%#Eval("TO_JHQ")%>' Width="90px"
                                                        OnTextChanged="TO_JHQ_Textchanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("TO_JHQ")%>' Visible="false"></asp:Label>--%>
                                                    <asp:Label ID="PIC_SUPPLYTIME" runat="server" Text='<%#Eval("PIC_SUPPLYTIME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                <%--<asp:TextBox ID="TO_SJJHQ" runat="server" Text='<%#Eval("TO_SJJHQ")%>' Width="90px"
                                                        OnTextChanged="TO_SJJHQ_Textchanged" AutoPostBack="true"></asp:TextBox>--%>
                                                        <asp:Label ID="BJSJ" runat="server" Text='<%#Eval("BJSJ")%>' ></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="TO_UWGHT" runat="server" Text='<%#Eval("TO_UWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("PIC_ZXNUM")%>'></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    <asp:Label ID="TO_MONEY" runat="server" Text='<%#Eval("TO_MONEY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TO_NOTE" runat="server" Text='<%#Eval("TO_NOTE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("PIC_SHEETNO")%>'></asp:Label>
                                                </td>
                                               

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="28" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                û�����ݣ�</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
 <script language="javascript" type="text/javascript">
     //                                    var myST = new superTable("tab", {
     //                                        cssSkin: "tDefault",
     //                                        headerRows: 1,
     //                                        fixedCols: 0,
     //                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
     //                                        onStart: function() {
     //                                            //		        this.start = new Date();
     //                                        },
     //                                        onFinish: function() {
     //                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
     //                                        }
     //                                    });
                                </script>

                            </div>
                        </div>
                    </div>
                </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
              </asp:Content>