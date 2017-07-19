<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_ServiceSolve.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceSolve"
    Title="顾客服务处理表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务处理表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="提 交" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" OnClick="btnreturn_Click" Text="返 回" />
        &nbsp;&nbsp;&nbsp;
    </div>
    <div class="box-wrapper1">
        <div class="box-outer" style="text-align: center; width: 99%">
            <div style="text-align: center; margin-top: 25px">
                <h2>
                    顾客服务处理表</h2>
            </div>
            <table width="800px" style="margin: auto">
                <tr>
                    <td style="text-align: right; font-size: medium">
                        文件号：TJZJ-R-M-10
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; font-size: medium">
                        编号：<asp:Label runat="server" ID="lb_bh"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                    </td>
                    <td style="text-align: right">
                        制单人：<asp:Label runat="server" ID="zdr"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto">
                <asp:Panel ID="panel" runat="server">
                    <tr>
                        <td width="100px">
                            项目名称：
                        </td>
                        <td>
                            <asp:Label ID="CM_PJNAME" runat="server" Width="120px"></asp:Label>
                        </td>
                        <td width="100px">
                            合同号：
                        </td>
                        <td>
                            <asp:Label ID="CM_CONTR" runat="server" Width="120px"></asp:Label>
                        </td>
                        <td width="100px">
                            设备名称：
                        </td>
                        <td>
                            <asp:Label ID="CM_EQUIP" runat="server" Width="120px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            顾客名称：
                        </td>
                        <td>
                            <asp:Label ID="CM_CUSNAME" runat="server"></asp:Label>
                        </td>
                        <td>
                            信息简介：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="CM_INFO" runat="server" Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: left">
                            非到现场服务的，服务过程，原因分析及结果：<br /><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_URESULT" runat="server" Height="100px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: left">
                            需要到现场服务的，方案及要求：<br /><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_RESULT" runat="server" Height="100px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        附件上传：
                    </td>
                    <td colspan="5" align="center">
                        <asp:FileUpload ID="FileUp" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                            Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                        <br />
                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                        <div align="center">
                            <asp:GridView ID="GridView" runat="server" CellPadding="4" CssClass="toptable grid"
                                AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                Width="80%">
                                <Columns>
                                    <asp:BoundField DataField="ShowName" HeaderText="文件名称">
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
                                        <ControlStyle Font-Size="Small" />
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下载">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                ToolTip="下载" />
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
            </table>
            <br />
        </div>
    </div>
</asp:Content>
