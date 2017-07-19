<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PD_DocTypeIn.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.PD_DocTypeIn" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    投标文件评审管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">

        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" style="float: right">
                <asp:Button ID="cancel" runat="server" Text="返 回" OnClick="Back_Click" CausesValidation="False" />&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:HiddenField ID="HiddenFieldContent" runat="server" />
            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td width="30%" align="right">
                        投标基本信息：
                    </td>
                    <td width="70%" class="category">
                        <table style="width: 100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td width="30%" align="center">
                                    项目名称：
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="conName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        ControlToValidate="conName" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    设备名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtengnm" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="conName" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    业主：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtyz" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ControlToValidate="conName" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    金额：
                                </td>
                                <td>
                                    <asp:TextBox ID="tbje" runat="server" onblur="Arabia_to_Chinese(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="conName" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    次数：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    备注：
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtmeno" runat="server" TextMode="MultiLine" Rows="5" Width="65%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        投标文件上传：
                    </td>
                    <td class="category">
                        <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                            Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                        <br />
                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                        <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid"
                            AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                            Width="50%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="fileName" HeaderText="文件名称">
                                    <ControlStyle Font-Size="Small" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间">
                                    <ControlStyle Font-Size="Small" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                            Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                            ToolTip="删除" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Size="Small" />
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                            OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                            ToolTip="下载" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Size="Small" />
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                                Height="10px" />
                            <RowStyle BackColor="#EFF3FB" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        指定投标评审人员：
                    </td>
                    <td class="category">
                        <asp:Panel ID="Panel1" runat="server" EnableViewState="False">
                            <asp:Label ID="errorlb" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td class="category">
                        <asp:Button ID="submint" runat="server" Text="提 交" OnClick="submint_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
