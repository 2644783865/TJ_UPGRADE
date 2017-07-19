<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_PJinfo.aspx.cs" Inherits="testpage.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Ӫ�ƻ�������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function aa() {
            $("#<%=GridView1.ClientID%> tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            ���״̬:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbl_mytask" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged">
                                <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                <asp:ListItem Text="�ҵ���������" Value="1" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                                RepeatDirection="Horizontal" CellSpacing="20" TextAlign="Right">
                                <asp:ListItem Text="��ʼ��" Value="0"></asp:ListItem>
                                <asp:ListItem Text="�����" Selected="True" Value="1"></asp:ListItem>
                                <asp:ListItem Text="�Ѳ���" Value="3"></asp:ListItem>
                                <asp:ListItem Text="��ͨ��" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLink" NavigateUrl="CM_AddTask.aspx?action=add" runat="server">
                                <asp:Image ID="ImageTo" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                ��������</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            �Ƶ�ʱ��:
                            <asp:TextBox runat="server" ID="txt_Data"></asp:TextBox>
                        </td>
                        <td>
                            ҵ������:
                            <asp:TextBox runat="server" ID="txt_YeZhu"></asp:TextBox>
                        </td>
                        <td>
                            ҵ����ͬ��:
                            <asp:TextBox runat="server" ID="txt_HeTong"></asp:TextBox>
                        </td>
                        <td style="width: 100px">
                            ��ѡ���ѯ���ͣ�
                        </td>
                        <td valign="middle" style="width: 120px">
                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                <asp:ListItem Text="-��ѡ��-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="��ͬ���" Value="1"></asp:ListItem>
                                <asp:ListItem Text="�豸����" Value="2"></asp:ListItem>
                                <asp:ListItem Text="��Ŀ����" Value="3"></asp:ListItem>
                                <asp:ListItem Text="ͼ��" Value="4"></asp:ListItem>
                                <asp:ListItem Text="�Ƶ���" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="searchcontent" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" Text="��  ѯ" OnClick="btn_Search_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%;">
            <div style="width: 100%; overflow: scroll">
                <asp:GridView ID="GridView1" Width="1600px" CssClass="toptable grid" Style="white-space: normal"
                    runat="server" OnRowDataBound="GridView1_OnRowDataBound" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text=' <%#Eval("ID_Num")%>'></asp:Label>
                                <asp:HiddenField ID="id" runat="server" Value='<%#Eval("ID") %>' />
                                <asp:HiddenField ID="status" runat="server" Value='<%#Eval("CM_SPSTATUS") %>' />
                                <asp:HiddenField ID="CM_CANCEL" runat="server" Value='<%#Eval("CM_CANCEL") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CM_COMP" HeaderText="ҵ������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CM_CONTR" HeaderText="��ͬ��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="TSA_ID" HeaderText="�����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>--%>
                        <asp:BoundField DataField="CM_DFCONTR" HeaderText="ҵ����ͬ��" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CM_PROJ" HeaderText="��Ŀ����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TSA_ENGNAME" HeaderText="��Ʒ����" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%-- <asp:BoundField DataField="TSA_MAP" HeaderText="ͼ��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TSA_NUMBER" HeaderText="��Ŀ" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TSA_UNIT" HeaderText="��λ" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>--%>
                        <asp:BoundField DataField="CM_FHDATE" HeaderText="������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CM_ZDTIME" HeaderText="�Ƶ�����" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="�Ƶ���">
                            <ItemTemplate>
                                <asp:Label ID="lb_zdr" runat="server" Text='<%# Eval("CM_NAME")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���״̬">
                            <ItemTemplate>
                                <asp:Label ID="lb_status" runat="server" Text='<%# Eval("CM_SPSTATUS").ToString()=="1"?"�����":Eval("CM_SPSTATUS").ToString()=="2"?"���ͨ��":Eval("CM_SPSTATUS").ToString()=="0"?"��ʼ��":"������"%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�鿴" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTask_look" Target="_blank" CssClass="link" NavigateUrl='<%#"CM_TaskPinS.aspx?action=look&id="+Eval("ID") %>'
                                    runat="server">
                                    <asp:Image ID="InfoImage_look" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />
                                    �鿴
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTask_ps" CssClass="link" NavigateUrl='<%#"CM_TaskPinS.aspx?action=ps&id="+Eval("ID") %>'
                                    runat="server">
                                    <asp:Image ID="InfoImage_ps" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />
                                    ����
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ɾ�����޸�" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <span onclick="javascript:return confirm('�޸ĺ������ʷ\r������¼���¿�ʼ������\r\r�����޸���');">
                                    <asp:HyperLink ID="lnkEdit" NavigateUrl='<%# "CM_AddTask.aspx?action=edit&id="+Eval("ID") %>'
                                        runat="server">
                                        <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                            hspace="2" align="absmiddle" />�޸�</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ID")%>'
                                    OnClick="lnkDelete_OnClick" OnClientClick="return confirm('ȷ��ɾ����?')">
                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />ɾ��</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="21px" Width="150px" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    û�м�¼!</asp:Panel>
            </div>
        </div>
        <div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
