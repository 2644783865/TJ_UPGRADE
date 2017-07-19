<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_TaskPinS.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_TaskPinS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Ӫ�ƻ�������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tb
        {
            text-align: center;
            width: 400px;
        }
    </style>
    <asp:HiddenField runat="server" ID="UserID" />
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="LbtnYes" runat="server" OnClientClick="javascript:return confirm('ȷ���ύ��');"
                                    OnClick="btnYes_Click">
                                    <asp:Image ID="Image3" Style="cursor: hand" ToolTip="ͬ�Ⲣ�ύ" ImageUrl="~/Assets/icons/positive.gif"
                                        Height="18" Width="18" runat="server" />
                                    ͬ��
                                </asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('ȷ�ϲ�����');"
                                    OnClick="btnNO_Click">
                                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="���ز��ύ" ImageUrl="~/Assets/icons/delete.gif"
                                        Height="18" Width="18" runat="server" />
                                    ����
                                </asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LbtnBack" runat="server" CausesValidation="False" OnClick="btn_back_Click">
                                    <asp:Image ID="Image7" Style="cursor: hand" ToolTip="����" ImageUrl="~/Assets/icons/back.png"
                                        Height="17" Width="17" runat="server" />
                                    ����
                                </asp:LinkButton>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="��ͬ��Ϣ" TabIndex="0">
                                <HeaderTemplate>
                                    �ƻ�����Ϣ
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel" runat="server">
                                        <div style="width: 85%; margin: 0px auto;">
                                            <div style="font-size: x-large; text-align: center; padding: 15px; font-weight: bold">
                                                ��&nbsp;Ӫ&nbsp;��&nbsp;��&nbsp;��</div>
                                            <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="800px">
                                                <tr>
                                                    <td style="width: 120px">
                                                        ��ţ�
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_ID" runat="server" Width="24px"></asp:Label>
                                                    </td>
                                                    <td style="width: 120px">
                                                        �ļ��ţ�
                                                    </td>
                                                    <td style="background-color: White">
                                                        TJZJ-R-M-04
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ������λ��
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_COMP" runat="server" Width="240px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        ��Ŀ���ƣ�
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_PROJ" runat="server" Width="240px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ��ͬ�ţ�
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_CONTR" runat="server" Width="240px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        �Է���ͬ��
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_DFCONTR" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        �����
                                                    </td>
                                                    <td style="background-color: White">
                                                        <asp:Label ID="CM_MANCLERK" runat="server"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ��ע��
                                                    </td>
                                                    <td style="background-color: White" colspan="3">
                                                        <asp:TextBox ID="TSA_NOTE" runat="server" TextMode="MultiLine" Width="80%" Rows="5"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                                border="1" style="white-space: normal">
                                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle headcolor">
                                                            <td style="width: 100px">
                                                                <strong>���</strong>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <strong>�����</strong>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <strong>��Ʒ����</strong>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <strong>ͼ��</strong>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <strong>����</strong>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <strong>��λ</strong>
                                                            </td>
                                                            <td>
                                                                <strong>����</strong>
                                                            </td>
                                                            <td>
                                                                <strong>��ע</strong>
                                                            </td>
                                                            <td style="width: 150px">
                                                                <strong>�豸����</strong>
                                                            </td>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id="tr1" runat="server" class="baseGadget" onmouseover="this.className='highlight'"
                                                            onmouseout="this.className='baseGadget'" style="height: 21px">
                                                            <td>
                                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                                <asp:HiddenField ID="TSA_CANCEL" runat="server" Value='<%#Eval("TSA_CANCEL") %>' />
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_ID")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_ENGNAME")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_MAP")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_NUMBER")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_UNIT")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="TSA_MATERIAL" Text='<%#Eval("TSA_MATERIAL") %>' Width="180px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="TSA_IDNOTE" Text='<%#Eval("TSA_IDNOTE") %>' Width="180px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TSA_TYPENAME") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="white-space: normal">
                                                <tr>
                                                    <td style="width: 130px">
                                                        �������ڣ�
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_FHDATE" CssClass="tb" runat="server" Width="400px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ������׼��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_LEVEL" CssClass="tb" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ����У�������գ�
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_TEST" CssClass="tb" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        �����ص㼰���䷽ʽ��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_JHADDRESS" CssClass="tb" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ��װҪ��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_BZ" CssClass="tb" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ����Ҫ��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_JH" runat="server" CssClass="tb"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ����Ҫ��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_YQ" runat="server" CssClass="tb"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        �������ˣ�
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_DUTY" runat="server" CssClass="tb"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ��ע��
                                                    </td>
                                                    <td style="text-align: center; background-color: White;">
                                                        <asp:Label ID="CM_NOTE" CssClass="tb" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        �������أ�
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="toptable grid"
                                                            AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                                            Width="50%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="���">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Wrap="False" />
                                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="showName" HeaderText="�ļ�����">
                                                                    <ControlStyle Font-Size="Small" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fileUpDate" HeaderText="�ļ��ϴ�ʱ��">
                                                                    <ControlStyle Font-Size="Small" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="����">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="tb_files" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                                            OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                                            OnInit="imgbtnDF_Init" ToolTip="����" />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Size="Small" />
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                                                                Height="10px" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel runat="server" ID="Pan_ShenHe" HeaderText="������Ϣ" TabIndex="1">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000;">
                                        <asp:Panel ID="Pan1" runat="server">
                                            <table width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                                <tr>
                                                    <td width="110px" height="25" align="center">
                                                        ������Ϣ��
                                                    </td>
                                                    <td class="category">
                                                        <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                                            border="1">
                                                            <tr>
                                                                <td>
                                                                    �Ƶ��ˣ�
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lb_Zdr" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    �Ƶ��������
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_zdryj" Columns="100" Rows="4" runat="server" TextMode="MultiLine"
                                                                        ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
