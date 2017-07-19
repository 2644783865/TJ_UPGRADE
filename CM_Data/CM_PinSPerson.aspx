<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_PinSPerson.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_PinSPerson" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ������Ա����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function Add_info() {
            var dep = document.getElementById('<%=DDlItem.ClientID%>');
            var index = dep.selectedIndex;
            var value = dep.options[index].value;
            if (value == "a") {
                alert("��ѡ���������");
            }
            else {
                var arr = new Array();
                arr.push("CM_PinSAdd.aspx?Action=");
                arr.push(value);
                window.showModalDialog(arr.join(''), '', "dialogHeight:400px;dialogWidth:650px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left" style="width: 10%;" valign="middle">
                                ������Ա��Ϣ
                            </td>
                            <td align="right" valign="middle">
                                ������������:&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="dplPS" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplPS_SelectedIndexChanged">
                                    <asp:ListItem Text="ȫ��" Value="a" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="��ͬ����" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Ͷ������" Value="1" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="�ƻ�������" Value="2" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="�˿ͲƲ�" Value="4" Enabled="false"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="hylAddPS" CssClass="hand" runat="server">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />&nbsp;�����Ϣ</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="hylAddPS"
                                    PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-150">
                                </asp:PopupControlExtender>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="palPSHTLB" Style="visibility: hidden; border-style: solid; border-width: 1px;
                        border-color: blue; background-color: Menu;" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="180px;">
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
                                        <td align="left">
                                            �������
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDlItem" runat="server">
                                                <asp:ListItem Text="-��ѡ��-" Value="a"></asp:ListItem>
                                                <asp:ListItem Text="��ͬ����" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Ͷ������" Value="1" Enabled="false"></asp:ListItem>
                                                <asp:ListItem Text="�ƻ�������" Value="2" Enabled="false"></asp:ListItem>
                                                <asp:ListItem Text="�˿ͲƲ�" Value="4" Enabled="false"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <br />
                                            <input type="button" value="ȷ ��" onclick="Add_info()" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvPS" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="ID_NUM" HeaderText="���" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="������">
                                <ItemTemplate>
                                    <asp:Label ID="lb_PinS" runat="server" Text='<%#Eval("per_type").ToString()=="0"?"��ͬ����":Eval("per_type").ToString()=="1"?"Ͷ������":Eval("per_type").ToString()=="2"?"��������":Eval("per_type").ToString()=="4"?"�˿ͲƲ�":"" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DEP_NAME" HeaderText="��������" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="ST_NAME" HeaderText="������Ա" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="ST_POSITION" HeaderText="��λ" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="State" HeaderText="�˺�״̬" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ɾ��" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("id")+","+Eval("per_type") %>'
                                        OnClick="Lbtn_Del_Click" OnClientClick="javascript:return confirm('ȷ��Ҫɾ����');">
                                        <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                        <hr style="width: 100%; height: 0.1px; color: Blue;" />
                        û�м�¼!</asp:Panel>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dplPS" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
