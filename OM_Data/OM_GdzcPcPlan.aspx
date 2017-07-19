<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_GdzcPcPlan.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcPcPlan" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �ɹ��ƻ���ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                           <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                            <table width="100%">
                                <tr>
                                    <%--<td>
                                        <asp:RadioButtonList ID="rblXiaTui" runat="server" RepeatColumns="2" TextAlign="Right"
                                            AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblXiaTui_OnSelectedIndexChanged">
                                            <asp:ListItem Text="δ����" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="������" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>--%>
                                    <td align="right">
                                        ����״̬��
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblSPZT" runat="server" RepeatColumns="7" TextAlign="Right"
                                            AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSPZT_OnSelectedIndexChanged">
                                            <asp:ListItem Text="ȫ��" Value="" ></asp:ListItem>
                                            <asp:ListItem Text="δ�ύ" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="���ύ" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="������" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="��ͨ��" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="�Ѳ���" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="�ҵ��������" Value="7" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnDelete" runat="server" Text="ɾ��" OnClick="btnDelete_OnClick" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hplAddPC" NavigateUrl="~/OM_Data/OM_GdzcPcDetail.aspx?action=add"
                                            CssClass="hand" runat="server">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />
                                            �����ɹ�����</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer" style="width: 100%; overflow: auto;">
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  OnRowDataBound="GridView1_OnRowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                            <RowStyle BackColor="#EFF3FB"  />
                            <Columns>
                                <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                        <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" Visible="false" runat="server" onclick="checkme(this)"
                                            Checked="false"></asp:CheckBox>
                                            <input type="hidden" id="hidInCode" runat="server" value='<%#Eval("INCODE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="���뵥��" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DEPARTMENT" HeaderText="���벿��" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <%--<asp:BoundField DataField="NAME" HeaderText="����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MODEL" HeaderText="�ͺŻ����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="NUM" HeaderText="����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="XQTIME" HeaderText="����ʱ��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />--%>
                                <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgent" runat="server" Text='<%#Eval("AGENT") %>'></asp:Label>
                                        <asp:Label ID="lblAgentid" runat="server" Text='<%#Eval("AGENTID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ADDTIME" HeaderText="����ʱ��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="����״̬" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("STATUS").ToString()=="0"?"δ�ύ":Eval("STATUS").ToString()=="1"?"���ύ":Eval("STATUS").ToString()=="3"||Eval("STATUS").ToString()=="5"?"�Ѳ���":Eval("STATUS").ToString()=="6"?"��ͨ��":"�����" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�޸�" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplmod" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_GdzcPcDetail.aspx?action=mod&id="+Eval("CODE") %>'>
                                            <asp:Image ID="Image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            �޸�
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�鿴" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplview" runat="server" CssClass="link" NavigateUrl='<%#"OM_GdzcPcLook.aspx?action=look&id="+Eval("CODE") %>'>
                                            <asp:Image ID="Image3" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            �鿴
                                        </asp:HyperLink>
                                        <asp:label id="yiji" runat="server" visible="false" Text='<%#Eval("CARRVWAID") %>' />
                                        <asp:label id="erji" runat="server" visible="false" Text='<%#Eval("CARRVWBID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplreview" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_GdzcPcLook.aspx?action=review&id="+Eval("CODE") %>'>
                                            <asp:Image ID="Image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            ���
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server">
                            û�м�¼!</asp:Panel>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
