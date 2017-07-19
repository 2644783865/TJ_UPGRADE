<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"
    ValidateRequest="false" AutoEventWireup="true" CodeBehind="CM_CustoInList.aspx.cs"
    Inherits="ZCZJ_DPF.CM_Data.CM_CustoInList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �˿ͲƲ���ⵥ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // �������ؼ����ó���Ϊ table ���֣�Ĭ�����ã�������ʹ�������ּ�Ϊtd
            {
                container = container.parentNode.parentNode; // ��Σ� <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }

        $(function() {
            $("#content").blur(function() {
                $("#<%=Hid_ZJ.ClientID %>").val($("#content").html());
            });
            
            if ($("#<%=txtDeclare.ClientID %>").val() != "") {
                $("#content").html($("#<%=txtDeclare.ClientID %>").val());
            }

            //alert($("#<%=txtDeclare.ClientID %>").val());
            $("#content").blur(function() {
                $("#<%=txtDeclare.ClientID %>").val($("#content").html());
                //  alert($("#<%=txtDeclare.ClientID %>").val());
            });
        });
    </script>

    <script src="../JS/jmeditor/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="../JS/jmeditor/JMEditor.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <asp:HiddenField runat="server" ID="psr" />
    <asp:HiddenField runat="server" ID="zdr" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" Text="�� ��" Visible="false" OnClick="btnsubmit_Click" />
        <asp:Button ID="btnedit" runat="server" Text="�� ��" Visible="false" OnClick="btnedit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" Text="�� ��" OnClick="btnreturn_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper1" style="text-align: center">
                <div class="box-outer">
                    <div style="margin-top: 20px; padding-top: 10px">
                        <h2 style="font-size: x-large">
                            �˿ͲƲ���ⵥ</h2>
                    </div>
                    <table width="800px" style="margin: auto">
                        <tr>
                            <td style="text-align: right; font-size: small">
                                �ļ��ţ�TJZJ-R-M-15
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: small">
                                ��ţ�<asp:Label runat="server" ID="CM_BIANHAO" Width="150px" Text="GKFWCL13073"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;�� ����1
                                <asp:TextBox runat="server" ID="txtDeclare" Style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto">
                        <asp:Panel ID="panel" runat="server">
                            <tr>
                                <td width="150px">
                                    �˿����ƣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_COSTERM" runat="server"></asp:TextBox>
                                </td>
                                <td width="150px">
                                    ��ͬ�ţ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_CONTR" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ��Ŀ���ƣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_PJNAME" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    �豸���ƣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_EQUIP" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���������ƣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_APPNAME" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    �������ڣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_CMDATE" runat="server" onclick="setday(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" height="100px">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; ������ݣ�&nbsp;
                                    <asp:TextBox ID="CM_INCONT" runat="server" Width="300px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                    ������&nbsp;
                                    <asp:TextBox ID="CM_NUM" runat="server" Width="80px"></asp:TextBox>
                                    <br />
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; ����λ�ã�&nbsp;
                                    <asp:TextBox ID="CM_PLACE" runat="server"></asp:TextBox><br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �Ƶ��������
                                </td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="CM_ZDYJ" TextMode="MultiLine" Width="500px" Height="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="CM_SFHG" runat="server">
                                        <asp:ListItem Text="��ѡ��" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="ͨ��" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="��ͨ��" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                ������Ϣ��
                            </td>
                            <td colspan="3">
                                <asp:FileUpload ID="FileUp" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                    Text="�ϴ��ļ�" OnClick="btnUp_Click" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <div align="center">
                                    <asp:GridView ID="GridView" runat="server" CellPadding="4" CssClass="toptable grid"
                                        AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                        Width="80%">
                                        <Columns>
                                            <asp:BoundField DataField="ShowName" HeaderText="�ļ�����">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fileUpDate" HeaderText="�ļ��ϴ�ʱ��">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="ɾ��">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                        Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                                        ToolTip="ɾ��" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Size="Small" />
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/download.jpg"
                                                        OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                        ToolTip="����" />
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
                                </div>
                            </td>
                        </tr>
                        <asp:Panel runat="server" ID="panel1">
                            <tr>
                                <td>
                                    У�����ݣ�
                                </td>
                                <td colspan="3" align="left">
                                    <asp:CheckBoxList ID="CM_TEST" runat="server" TextAlign="Left" RepeatDirection="Horizontal"
                                        RepeatLayout="Table" Width="300px">
                                        <asp:ListItem Value="0" Text="�ߴ����"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="�������"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="��������"></asp:ListItem>
                                    </asp:CheckBoxList>
                                    ���������ʼ첿ȷ����
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; ����������������ϸ�������ڵ��������⣩<br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <div id="content" contenteditable="true" class="editDemo" style="overflow: scroll;
                                        height: 120px; width: 90%; border: 1px #B3CDE8 solid;" align="left">
                                        <%=declare%>
                                    </div>
                                    <asp:HiddenField runat="server" ID="Hid_ZJ" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �ʼ�Ա
                                </td>
                                <td>
                                    <asp:TextBox ID="ST_ZJ" runat="server"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdzj" />
                                    <asp:DropDownList ID="CM_CHECK" runat="server">
                                        <asp:ListItem Text="��ѡ��" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="ͨ��" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="��ͨ��" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_ZJDATE" runat="server" class="easyui-datebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���Ա
                                </td>
                                <td>
                                    <asp:TextBox ID="ST_KG" runat="server"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdkg" />
                                    <asp:CheckBox ID="CM_BTIN" runat="server" /><label for="ctl00_PrimaryContent_CM_BTIN">ȷ�����</label>
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_KGDATE" runat="server" class="easyui-datebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ������
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="CM_RKYJ" TextMode="MultiLine" Width="500px" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                ������Ϣ
                            </td>
                            <td colspan="3">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Button ID="btnFU" runat="server" Text="�ϴ��ļ�" OnClick="btnFU_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError1" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="80%">
                                    <asp:Repeater runat="server" ID="rptGKCC_ZJ">
                                        <HeaderTemplate>
                                            <tr style="background-color: #71C671;">
                                                <td>
                                                    <strong>�ļ�����</strong>
                                                </td>
                                                <td>
                                                    <strong>�ļ��ϴ�ʱ��</strong>
                                                </td>
                                                <td>
                                                    <strong>ɾ��</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_SHOWNAME" Text='<%#Eval("FILE_SHOWNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_UPDATE" Text='<%#Eval("FILE_UPDATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtndelete1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete1_OnClick" CausesValidation="False" Text="ɾ��">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload1_OnClick" CausesValidation="False" Text="����">
                                                        <asp:Image runat="server" ID="imgonload" ImageUrl="~/Assets/images/download.jpg" Height="15px"
                                                            Width="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddFU" />
            <asp:PostBackTrigger ControlID="btnFU" />
            <asp:PostBackTrigger ControlID="rptGKCC_ZJ" />
            <asp:PostBackTrigger ControlID="GridView" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
