<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_InventorySchema.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventorySchema" Title="�̵㷽��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <title>�̵㷽��</title>
</head>
<body>

    <script type="text/javascript" language="javascript">
    function canceltip() {
        var retVal = confirm("������ǰѡ�����������أ�");
        return retVal;
    }

    function checkPage() {
        var retVal = confirm("ȷ������ѡ���������̵㷽����");
        if (retVal == false) {
            return retVal;
        }
        
//        �̵���
//���ж��̵���ʱ��ѡ��Ȼ�����ж��Ƶ��ˣ�������ж��̵�ʱ��
//        var ddlc = document.getElementById("<%=DropDownListClerk.ClientID %>");
//        var clerkcode = "";
//        for (var i = 0; i < ddlc.options.length; i++) {
//            if (ddlc.options[i].selected == true) {
//                clerkcode = ddlc.options[i].value;
//                break;
//            }
//        }
        
//        if (clerkcode != "") {
////            return true;            
//            var ddlz=document.getElementById("<%=DropDownListZDR.ClientID %>");
//            var zdrcode="";
//            for (var i = 0; i < ddlz.options.length; i++) {
//                if (ddlz.options[i].selected == true) {
//                    zdrcode = ddlz.options[i].value;
//                    break;
//                }
//            }
//            if (zdrcode != "") {
//                return true;
//            }
//            else {
//                alert("��ѡ���Ƶ��ˣ�");
//                return false;
//            }
//        }
//        else {
//            alert("��ѡ���̵��ˣ�");
//            return false;
//        }
      }

    </script>

    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div>
        <asp:Image ID="Image1" ImageUrl="~/Assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg"
            runat="server" />
        <div class="RightContentTitle">
        </div>
    </div>
    <div class="RightContent">
        <div class="box-wrapper">
            <div class="box-outer">
                <%--<asp:Panel ID="PanelSchema" runat="server" Width="100%" Visible="true" ScrollBars="Auto">--%>
                    <table cellpadding="4px" width="100%">
                        <caption style="font-size: large; font-weight: bold">
                            �̵㷽��</caption>
                        <thead>
                            <tr>
                                <th colspan="5">
                                    <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" width="35%">
                                    �ƶ����ڣ�<asp:Label ID="Date" runat="server"></asp:Label>
                                    <asp:Label ID="Code" runat="server" Visible="false"></asp:Label>
                                </th>
                                <th width="30%">
                                    �����ƶ��ˣ�<asp:Label ID="Planer" runat="server"></asp:Label>
                                </th>
                                <th colspan="2" width="35%" align="left">
                                    �������ͣ�<asp:DropDownList ID="DropDownListType" runat="server">
                                        <asp:ListItem Value="0">��ʱ���</asp:ListItem>
                                        <asp:ListItem Value="1">�������</asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="2" width="35%">
                                    &nbsp;&nbsp;&nbsp;&nbsp; �̵��ˣ�<asp:DropDownList ID="DropDownListClerk" runat="server">
                                    </asp:DropDownList>
                                </th>
                                <th width="30%">
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;�Ƶ���:
                                    <asp:DropDownList ID="DropDownListZDR" runat="server">
                                    </asp:DropDownList>
                                </th>
                                <th colspan="2" width="35%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��ע��<asp:TextBox ID="Comment" runat="server"></asp:TextBox>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                                <td width="20%">
                                </td>
                            </tr>
                           
                                    <tr>
                                        <td align="center" width="20%" style="font-weight: bold">
                                            �ܿ�
                                        </td>
                                        <td colspan="4" width="80%">
                                            <asp:CheckBoxList ID="CheckBoxListRootWarehouse" runat="server" RepeatColumns="10"
                                                RepeatDirection="Horizontal" RepeatLayout="Table" CellPadding="2" AutoPostBack="true"
                                                OnSelectedIndexChanged="CheckBoxListRootWarehouse_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="20%" style="font-weight: bold">
                                            �̵�ֿ�
                                        </td>
                                        <td colspan="4" width="80%">
                                            <asp:CheckBoxList ID="CheckBoxListWarehouse" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                                RepeatLayout="Table" CellPadding="2">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                
                            
                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                <tr>
                                    <td align="center" width="20%" style="font-weight: bold">
                                        �������
                                    </td>
                                    <td colspan="4" width="80%">
                                        <asp:CheckBoxList ID="CheckBoxListMaterialType" runat="server" RepeatColumns="10"
                                            RepeatDirection="Horizontal" RepeatLayout="Table" CellPadding="2">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="PanelMar" runat="server" Visible="true">
                                <tr>
                                    <td align="center" width="20%" style="font-weight: bold">
                                        �̵���Ŀ
                                    </td>
                                    <td colspan="4" width="80%">
                                        <asp:CheckBoxList ID="CheckBoxListEng" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                            RepeatLayout="Table" CellPadding="2">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="20%" style="font-weight: bold">
                                        ���ϱ���
                                    </td>
                                    <td colspan="4" width="80%">
                                        <table>
                                            <tr>
                                                <td>
                                                    ���ϱ���ӣ�
                                                    <asp:TextBox ID="TextBoxFromCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    ����
                                                    <asp:TextBox ID="TextBoxToCode" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ����ͺŴӣ�
                                                    <asp:TextBox ID="TextBoxFromStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    ����
                                                    <asp:TextBox ID="TextBoxToStandard" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="center" width="20%" style="font-weight: bold">
                                    ���ϱ���
                                </td>
                                <td colspan="4" width="80%">
                                    <div style="width: 50%">
                                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAdd" runat="server" Text="���" Visible="false" />
                                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" Text="����" OnClick="btnReset_Click" />
                                                <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                                    CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="�߼�">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                                <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                                    <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
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
                                                                    <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="5" align="center">
                                    <asp:Button ID="Comfirm" runat="server" Text="ȷ��" OnClick="Confirm_Click" OnClientClick=" return checkPage()" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Cancel" runat="server" Text="����" OnClick="Cancel_Click" OnClientClick="canceltip()" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </tfoot>
                    </table>
<%--                </asp:Panel>--%>
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
