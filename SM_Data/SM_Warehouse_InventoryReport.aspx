<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="SM_Warehouse_InventoryReport.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventoryReport" Title="�̵㱨��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPagingOfMS.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        CombineScripts="false" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>

    <script src="SM_JS/AllKeyControl1.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script type="text/javascript">    
        
         function checkPositive(tb) {
         tb.parentNode.parentNode.style.backgroundColor ='#EEF7FD';
             var current = parseFloat(tb.value);
             if ((isNaN(current))||(current < 0)) {
                 alert("����ȷ����������");
                 tb.value = 0;
             }
             Statistic(tb);
         }
         
         function checkPositive1(tb) {
         tb.parentNode.parentNode.style.backgroundColor ='#EEF7FD';
             var current = parseFloat(tb.value);
             if (isNaN(current)) {
                 alert("����ȷ����������");
                 tb.value = 0;
             }
             Statistic(tb);
         }
         function Statistic(tb) {
         
             var par = tb.parentNode.parentNode;

             var unitprice = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);//����

             var numinaccount = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);//����
             var fzninaccount = parseFloat(par.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);//��������
             var amountinaccount = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML);//���

             var numnotin = parseFloat(par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value);//������
             var fznnotin = parseFloat(par.getElementsByTagName("td")[19].getElementsByTagName("input")[0].value);
             var amountnotin = par.getElementsByTagName("td")[20].getElementsByTagName("input")[0];
             amountnotin.value = parseFloat((unitprice * numnotin).toFixed(2));

             var numnotout = parseFloat(par.getElementsByTagName("td")[21].getElementsByTagName("input")[0].value);
             var fznnotout = parseFloat(par.getElementsByTagName("td")[22].getElementsByTagName("input")[0].value);
             var amountnotout = par.getElementsByTagName("td")[23].getElementsByTagName("input")[0];
             amountnotout.value = parseFloat((unitprice * numnotout).toFixed(2));

             var duenum = par.getElementsByTagName("td")[24].getElementsByTagName("input")[0];
             duenum.value =  parseFloat((numinaccount + numnotin - numnotout).toFixed(4));
             var duefzn = par.getElementsByTagName("td")[25].getElementsByTagName("input")[0];
             duefzn.value = parseFloat((fzninaccount + fznnotin - fznnotout).toFixed(4));
             var dueamount = par.getElementsByTagName("td")[26].getElementsByTagName("input")[0];
             dueamount.value =  parseFloat((unitprice * (numinaccount + numnotin - numnotout)).toFixed(2));

//             var realnum = parseFloat(par.getElementsByTagName("td")[27].getElementsByTagName("input")[0].value);
//             var realfzn = parseFloat(par.getElementsByTagName("td")[28].getElementsByTagName("input")[0].value);
//             var realamount = par.getElementsByTagName("td")[29].getElementsByTagName("input")[0];
//             realamount.value = parseFloat((realnum * unitprice).toFixed(2));

//             var diffnum = par.getElementsByTagName("td")[30].getElementsByTagName("input")[0];
//             diffnum.value = parseFloat((realnum - (numinaccount + numnotin - numnotout)).toFixed(4));
//             var difffzn = par.getElementsByTagName("td")[31].getElementsByTagName("input")[0];
//             difffzn.value = parseFloat((realfzn - (fzninaccount + fznnotin - fznnotout)).toFixed(4));
//             var diffamount = par.getElementsByTagName("td")[32].getElementsByTagName("input")[0];
//             diffamount.value = parseFloat(((realnum - (numinaccount + numnotin - numnotout)) * unitprice).toFixed(2));

               var diffnum = parseFloat(par.getElementsByTagName("td")[30].getElementsByTagName("input")[0].value);
               
               var difffzn = parseFloat(par.getElementsByTagName("td")[31].getElementsByTagName("input")[0].value);
               
               var diffamount = parseFloat(par.getElementsByTagName("td")[32].getElementsByTagName("input")[0]);
               
               diffamount.value = parseFloat((diffnum * unitprice).toFixed(2));

               var realnum = par.getElementsByTagName("td")[27].getElementsByTagName("input")[0];
               realnum.value = parseFloat((diffnum + (numinaccount + numnotin - numnotout)).toFixed(4));
               var realfzn = par.getElementsByTagName("td")[28].getElementsByTagName("input")[0];
               realfzn.value = parseFloat((difffzn + (fzninaccount + fznnotin - fznnotout)).toFixed(4));
               var realamount = par.getElementsByTagName("td")[29].getElementsByTagName("input")[0];
               realamount.value = parseFloat((((diffnum+numinaccount + numnotin - numnotout)) * unitprice).toFixed(2));

             var tnotinnum = 0;
             var tnotinfzn = 0;
             var tnotinamount = 0;

             var tnotoutnum = 0;
             var tnotoutfzn = 0;
             var tnotoutamount = 0;

             var tduenum = 0;
             var tduefzn = 0;
             var tdueamount = 0;

             var trealnum = 0;
             var trealfzn = 0;
             var trealamount = 0;

             var tdiffnum = 0;
             var tdifffzn = 0;
             var tdiffamount = 0;
             
             var gv1 = document.getElementById("<%=GridView1.ClientID %>");
             for (i = 1; i < (gv1.rows.length - 3); i++) {
                 var val1 = gv1.rows[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value;
                 tnotinnum += parseFloat(val1);
                 var val2 = gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value;
                 tnotinfzn += parseFloat(val2);
                 var val3 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                 tnotinamount += parseFloat(val3);
                 var val4 = gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value;
                 tnotoutnum += parseFloat(val4);
                 var val5 = gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                 tnotoutfzn += parseFloat(val5);                 
                 var val6 = gv1.rows[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;
                 tnotoutamount += parseFloat(val6);
                 var val7 = gv1.rows[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value;
                 tduenum += parseFloat(val7);
                 var val8 = gv1.rows[i].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value;
                 tduefzn += parseFloat(val8);
                 var val9 = gv1.rows[i].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
                 tdueamount += parseFloat(val9);
                 var val10 = gv1.rows[i].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
                 trealnum += parseFloat(val10);
                 var val11 = gv1.rows[i].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value;
                 trealfzn += parseFloat(val11);                 
                 var val12 = gv1.rows[i].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value;
                 trealamount += parseFloat(val12);
                 var val13 = gv1.rows[i].getElementsByTagName("td")[30].getElementsByTagName("input")[0].value;
                 tdiffnum += parseFloat(val13);
                 var val14 = gv1.rows[i].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value;
                 tdifffzn += parseFloat(val14);                 
                 var val15 = gv1.rows[i].getElementsByTagName("td")[32].getElementsByTagName("input")[0].value;
                 tdiffamount += parseFloat(val15);
             }
             var lbtnotinnum = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[18].getElementsByTagName("span")[0];
             lbtnotinnum.innerHTML = tnotinnum.toFixed(4);
             var lbtnotinfzn = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[19].getElementsByTagName("span")[0];
             lbtnotinfzn.innerHTML = tnotinfzn.toFixed(4);
             var lbtnotinamount = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[20].getElementsByTagName("span")[0];
             lbtnotinamount.innerHTML = tnotinamount.toFixed(2);
             var lbtnotoutnum = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[21].getElementsByTagName("span")[0];
             lbtnotoutnum.innerHTML = tnotoutnum.toFixed(4);
             var lbtnotoutfzn = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[22].getElementsByTagName("span")[0];
             lbtnotoutfzn.innerHTML = tnotoutfzn.toFixed(4);             
             var lbtnotoutamount = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[23].getElementsByTagName("span")[0];
             lbtnotoutamount.innerHTML = tnotoutamount.toFixed(2);
             var lbtduenum = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[24].getElementsByTagName("span")[0];
             lbtduenum.innerHTML = tduenum.toFixed(4);
             var lbtduefzn = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[25].getElementsByTagName("span")[0];
             lbtduefzn.innerHTML = tduefzn.toFixed(4);
             var lbtdueamount = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[26].getElementsByTagName("span")[0];
             lbtdueamount.innerHTML = tdueamount.toFixed(2);
             var lbtrealnum = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[27].getElementsByTagName("span")[0];
             lbtrealnum.innerHTML = trealnum.toFixed(4);
             var lbtrealfzn = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[28].getElementsByTagName("span")[0];
             lbtrealfzn.innerHTML = trealfzn.toFixed(4);             
             var lbtrealamount = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[29].getElementsByTagName("span")[0];
             lbtrealamount.innerHTML = trealamount.toFixed(2);
             var lbtdiffnum = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[30].getElementsByTagName("span")[0];
             lbtdiffnum.innerHTML = tdiffnum.toFixed(4);
             var lbtdifffzn = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[31].getElementsByTagName("span")[0];
             lbtdifffzn.innerHTML = tdifffzn.toFixed(4);             
             var lbtdiffamount = gv1.rows[gv1.rows.length - 3].getElementsByTagName("td")[32].getElementsByTagName("span")[0];
             lbtdiffamount.innerHTML = tdiffamount.toFixed(2);
         }

         function exporttip() {
            var retVal = confirm("����Excelǰ���ȱ��棡�Ƿ񵼳���");
            return retVal;
        }

        function yinkui() {
            var id = document.getElementById('<%= LabelCode.ClientID %>').innerHTML;
            window.open("SM_Warehouse_InventoryReportFrom.aspx?ID="+ id);
        }

         function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
         }
        
        function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';}
    
    </script>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0">
        <cc1:TabPanel ID="Tab1" runat="server" HeaderText="�̵���ϸ" Height="480px" Width="100%">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="box-inner">
                            <div class="box_right">
                                <div class="box-title">
                                    <table width="98%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnShowPopup" runat="server" Text="ɸѡ"/>
                                                <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                                    PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                                    Y="10">
                                                </cc1:ModalPopupExtender>
                                                <asp:Button ID="Calculation" runat="server" Text="����" CausesValidation="False" OnClick="Calculation_Click" />
                                                <asp:Button ID="Uniprice" runat="server" Text="����" CausesValidation="False" OnClick="Uniprice_Click" Visible="false"/>
                                                <asp:Button ID="Save" runat="server" Text="����" CausesValidation="False" OnClick="Save_Click" />
                                                <asp:Button ID="Submit" runat="server" Text="�ύ" OnClick="Submit_Click" />&nbsp;
                                                <input id="btnYinKui" type="button" value="��ӯ�̿���" onclick="yinkui();" runat="server"
                                                    disabled="disabled" />&nbsp;
                                                <asp:Button ID="Export" runat="server" Text="����" OnClick="Export_Click" OnClientClick="return exporttip()" />
                                                <asp:Button ID="INExport" runat="server" Text="����ģ��" OnClick="INExport_Click" />
                                                <asp:Button ID="Adjust" runat="server" Text="����" OnClick="Adjust_Click" Visible="false" />
                                                <input id="Close" type="button" value="�ر�" onclick="closewin()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="PanelCondition" runat="server" Width="98%" Style="display: none">
                                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table width="83%" style="background-color: #CCCCFF; border: solid 1px black;">
                                                    <tr>
                                                        <td width="50%">
                                                            <table cellpadding="1">
                                                                <tr>
                                                                    <td style="white-space: nowrap;">
                                                                        ������ţ�
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="LabelfanCode"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="50%" align="right">
                                                            <asp:Button ID="btnQuery" runat="server" Text="��ѯ" OnClick="btnQuery_Click" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnClose" runat="server" Text="ȡ��" OnClick="btnClose_Click" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnReset" runat="server" Text="����" OnClick="btnReset_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%">
                                                            <table cellpadding="1">
                                                                <tr>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        �ƻ����ٺţ�
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        ���ϴ��룺
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxMaterialCode" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        �������ƣ�
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxMaterialName" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        ����ͺţ�
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        ���ʣ�
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxAttribute" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        ���꣺
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxGB" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        �ƻ�ģʽ��
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxPlanMode" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        �ֿ⣺
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxWarehouse" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        ��λ��
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxPosition" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="white-space: nowrap;" align="left">
                                                                        �߼�
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="50%" colspan="6">
                                                            <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                                                CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="�߼�">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                                            <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                                                <asp:ListItem Value="NO" Text=""></asp:ListItem>
                                                                                <asp:ListItem Value="OR" Selected="True">����</asp:ListItem>
                                                                                <asp:ListItem Value="AND">����</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="����">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                                            <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="�ȽϹ�ϵ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                                            <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                                                <asp:ListItem Value="NO" Text=""></asp:ListItem>
                                                                                <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                                                                <asp:ListItem Value="8">�����</asp:ListItem>
                                                                                <asp:ListItem Value="9">�Ұ���</asp:ListItem>
                                                                                <asp:ListItem Value="7">������</asp:ListItem>
                                                                                <asp:ListItem Value="1">����</asp:ListItem>
                                                                                <asp:ListItem Value="2">������</asp:ListItem>
                                                                                <asp:ListItem Value="3">����</asp:ListItem>
                                                                                <asp:ListItem Value="4">���ڻ����</asp:ListItem>
                                                                                <asp:ListItem Value="5">С��</asp:ListItem>
                                                                                <asp:ListItem Value="6">С�ڻ����</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="��ֵ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="box-wrapper">
                            <table style="width: 100%; border: solid 1px black;">
                                <tr>
                                    <td colspan="7">
                                        <asp:CheckBox ID="CheckBox20" runat="server" Text="�ƻ����ٺ�" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox21" runat="server" Text="���ϴ���" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox22" runat="server" Text="��������" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox23" runat="server" Text="����ͺ�" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox24" runat="server" Text="����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox25" runat="server" Text="����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="�ƻ�ģʽ" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="��(֧)��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="��δ�����(֧)��" AutoPostBack="true"
                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox7" runat="server" Text="��δ������(֧)��" AutoPostBack="true"
                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox8" runat="server" Text="Ӧ�����(֧)��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox9" runat="server" Text="�̵���(֧)��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox10" runat="server" Text="������(֧)��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox18" runat="server" Text="��׼��" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:CheckBox ID="CheckBox11" runat="server" Text="����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox12" runat="server" Text="���" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox13" runat="server" Text="��δ�����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox14" runat="server" Text="��δ������" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox15" runat="server" Text="Ӧ�����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox16" runat="server" Text="�̵���" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox17" runat="server" Text="������" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        <asp:CheckBox ID="CheckBox19" runat="server" Text="����" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ��һ�����ֶ�:
                                        <asp:DropDownList ID="DropDownListFirst" runat="server" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="PTC" Text="�ƻ����ٺ�" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="MaterialCode" Text="���ϴ���"></asp:ListItem>
                                            <asp:ListItem Value="MaterialName" Text="��������"></asp:ListItem>
                                            <asp:ListItem Value="Standard" Text="���"></asp:ListItem>
                                            <asp:ListItem Value="Attribute" Text="����"></asp:ListItem>
                                            <asp:ListItem Value="Location" Text="��λ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonListFirstType" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1">����</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        �ڶ������ֶ�:
                                        <asp:DropDownList ID="DropDownListSecond" runat="server" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="" Text="--��ѡ��--" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="PTC" Text="�ƻ����ٺ�"></asp:ListItem>
                                            <asp:ListItem Value="MaterialCode" Text="���ϴ���"></asp:ListItem>
                                            <asp:ListItem Value="MaterialName" Text="��������"></asp:ListItem>
                                            <asp:ListItem Value="Standard" Text="���"></asp:ListItem>
                                            <asp:ListItem Value="Attribute" Text="����"></asp:ListItem>
                                            <asp:ListItem Value="Location" Text="��λ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonListSecond" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1">����</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        ���������ֶ�:
                                        <asp:DropDownList ID="DropDownListThird" runat="server" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="" Text="--��ѡ��--" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="PTC" Text="�ƻ����ٺ�"></asp:ListItem>
                                            <asp:ListItem Value="MaterialCode" Text="���ϴ���"></asp:ListItem>
                                            <asp:ListItem Value="MaterialName" Text="��������"></asp:ListItem>
                                            <asp:ListItem Value="Standard" Text="���"></asp:ListItem>
                                            <asp:ListItem Value="Attribute" Text="����"></asp:ListItem>
                                            <asp:ListItem Value="Location" Text="��λ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonListThird" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1">����</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_save" runat="server" Text="����" OnClick="btn_save_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                                <asp:HiddenField ID="sumnuminaccount" runat="server" />
                                <asp:HiddenField ID="sumfzninaccount" runat="server" />
                                <asp:HiddenField ID="sumamountinaccount" runat="server" />
                                <asp:HiddenField ID="sumnotinnum" runat="server" />
                                <asp:HiddenField ID="sumnotinfznum" runat="server" />
                                <asp:HiddenField ID="sumnotinamount" runat="server" />
                                <asp:HiddenField ID="sumnotoutnum" runat="server" />
                                <asp:HiddenField ID="sumnotoutfznum" runat="server" />
                                <asp:HiddenField ID="sumnotoutamount" runat="server" />
                                <asp:HiddenField ID="sumduenum" runat="server" />
                                <asp:HiddenField ID="sumduefznum" runat="server" />
                                <asp:HiddenField ID="sumdueamount" runat="server" />
                                <asp:HiddenField ID="sumrealnum" runat="server" />
                                <asp:HiddenField ID="sumrealfznum" runat="server" />
                                <asp:HiddenField ID="sumrealamount" runat="server" />
                                <asp:HiddenField ID="sumdiffnum" runat="server" />
                                <asp:HiddenField ID="sumdifffznum" runat="server" />
                                <asp:HiddenField ID="sumdiffamount" runat="server" />
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: x-large; text-align: center;" colspan="4">
                                            �̵㱨��
                                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                                Visible="False" />
                                            <asp:Image ID="ImageAdjust" runat="server" ImageUrl="~/Assets/images/ADJUST.jpg"
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;������ţ�<asp:Label ID="LabelCode" runat="server"></asp:Label>
                                            <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            ϵͳ����ʱ�䣺<asp:Label ID="LabelTime" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2" align="right">
                                            <asp:HyperLink ID="hlksummary1" runat="server" Font-Size="Larger" Target="_blank">
                                                <asp:Image ID="InfoImage" ImageUrl="~/Assets/images/read.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />����1</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                            <asp:HyperLink ID="hlksummary2" runat="server" Font-Size="Larger" Target="_blank">
                                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/read.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />����2</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;�������ڣ�<asp:TextBox ID="TextBoxDate" Width="100px" runat="server"></asp:TextBox>
                                            <%--<cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                        Format="yyyy-MM-dd" Enabled="True">
                                    </cc1:CalendarExtender>--%>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ֿ����ƣ�<asp:Label ID="LabelWarehouse" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            �������ƣ�<asp:Label ID="LabelEng" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            �������ࣺ<asp:Label ID="LabelMar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelBody" runat="server" Height="400px" Width="100%" UpdateMode="Conditional">
                                <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1" EmptyDataText="û��������ݣ�" CssClass="toptable grid" ShowFooter="True"
                                    OnRowDataBound="GridView1_RowDataBound" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�ƻ����ٺ�" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="���ϴ���" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelMaterialCode" runat="server" Text="�ϼƣ�"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����ͺ�" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="TextBoxMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�ƻ�ģʽ" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�ֿ�" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��λ" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                                                <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��λ" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Number")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNumber" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��(֧)��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelFZN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FZN")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ�������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxNotInNumber" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container.DataItem, "NotInNumber")%>'
                                                    onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotInNumber" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ�����(֧)��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxNotInFZN" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container.DataItem, "NotInFZN")%>'
                                                    onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotInFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ�����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputNotInAmount" value='<%#DataBinder.Eval(Container.DataItem, "NotInAmount")%>'
                                                    readonly="readonly" runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotInAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ��������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxNotOutNumber" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container.DataItem, "NotOutNumber")%>'
                                                    onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotOutNumber" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ������(֧)��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxNotOutFZN" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container.DataItem, "NotOutFZN")%>'
                                                    onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotOutFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��δ������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputNotOutAmount" value='<%#DataBinder.Eval(Container.DataItem, "NotOutAmount")%>'
                                                    readonly="readonly" runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalNotOutAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ӧ�������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDueNumber" value='<%#DataBinder.Eval(Container.DataItem, "DueNumber")%>'
                                                    readonly="readonly" runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDueNumber" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ӧ�����(֧)��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDueFZN" value='<%#DataBinder.Eval(Container.DataItem, "DueFZN")%>'
                                                    readonly="readonly" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" runat="server" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDueFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ӧ�����" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDueAmount" value='<%#DataBinder.Eval(Container.DataItem, "DueAmount")%>'
                                                    readonly="readonly" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" runat="server" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDueAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�̵�����" HeaderStyle-Wrap="false" HeaderStyle-BackColor="#82c7b2"
                                            ItemStyle-BackColor="#82c7b2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                                    Width="60px" onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�̵���(֧)��" HeaderStyle-Wrap="false" HeaderStyle-BackColor="#82c7b2"
                                            ItemStyle-BackColor="#82c7b2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxRFZN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RFZN")%>'
                                                    Width="70px" onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalRFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�̵���" HeaderStyle-Wrap="false" HeaderStyle-BackColor="#82c7b2"
                                            ItemStyle-BackColor="#82c7b2">
                                            <ItemTemplate>
                                                <input type="text" id="InputRA" value='<%#DataBinder.Eval(Container.DataItem, "RA")%>'
                                                    readonly="readonly" runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalRA" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDiffNum" value='<%#DataBinder.Eval(Container.DataItem, "DiffNum")%>'
                                                    runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive1(this)"
                                                    style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDiffNum" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������(֧)��" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDiffFZN" value='<%#DataBinder.Eval(Container.DataItem, "DiffFZN")%>'
                                                    runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkPositive1(this)"
                                                    style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDiffFZN" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <input type="text" id="InputDiffAmount" value='<%#DataBinder.Eval(Container.DataItem, "DiffAmount")%>'
                                                    readonly="readonly" runat="server" onkeyup="grControlFocus(this)" onclick="getSelect(this)"
                                                    onblur="checkPositive(this)" style="width: 60px;" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LabelTotalDiffAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��ע" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxComment" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                                    onkeyup="grControlFocus(this)" onclick="getSelect(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                    <RowStyle BackColor="#EEF7FD" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5" />
                                </yyc:SmartGridView>
                                <asp:Panel ID="NoDataPanel" runat="server">
                                    û������!</asp:Panel>
                            </asp:Panel>
                            <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" />
                            <uc2:UCPaging ID="UCPaging2" runat="server" Visible="false" />
                            <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;����ˣ�<asp:DropDownList ID="DropDownListManager" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;�̵��ˣ�<asp:Label ID="LabelClert" runat="server"></asp:Label>
                                            <asp:Label ID="LabelClertCode" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;�����ˣ�<asp:DropDownList ID="DropDownListKeeping" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 25%">
                                            &nbsp;&nbsp;&nbsp;�Ƶ��ˣ�<asp:Label ID="LabelDoc" runat="server" Enabled="False"></asp:Label>
                                            <asp:Label ID="LabelDocCode" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Export" />
                        <asp:PostBackTrigger ControlID="INExport" />
                        <asp:AsyncPostBackTrigger ControlID="btnQuery" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    DisplayAfter="1">
                    <ProgressTemplate>
                        <img src="../Assets/images/ajaxloader.gif" style="position: absolute; left: 50%;
                            top: 50%;" alt="loading" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="Tab2" runat="server" HeaderText="�̵����" Width="100%">
            <ContentTemplate>
                <div>
                    <div>
                        <p style="color: Black; text-align: center; font-size: x-large;">
                            �̵㱨�����<asp:Image ID="ImageVerify2" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" /></p>
                    </div>
                    <div>
                        <table style="border: solid 1px black;">
                            <tr style="border: solid 1px black; width: 100%;">
                                <td style="border-right: solid 1px black; width: 30%;" rowspan="2">
                                    <p style="text-align: center;">
                                        һ�����</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxAdvice" runat="server" BorderStyle="None" Height="200px"
                                        Width="1000px" Style="overflow: auto;" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    ����ˣ�<asp:Label ID="LabelVerifier" runat="server" BorderStyle="None" Width="100px"></asp:Label>
                                    <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp; 
                                    ���ڣ�<asp:Label ID="LabelApproveDate" runat="server" BorderStyle="None" Width="100px"></asp:Label>
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Verify" runat="server" Text="ͬ��" OnClick="Verify_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Deny" runat="server" Text="����" OnClick="Deny_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="AntiVerify" runat="server" Text="����" OnClick="AntiVerify_Click" Enabled="false" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
