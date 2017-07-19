<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Service.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SERVICE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/StyleControl.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function autoHT() {
            var htbh = document.getElementById("<%=CM_CONTR.ClientID%>").value;
            $.ajax({
                url: 'GetData.ashx',
                type: 'get',
                data: "CM_CONTR=" + htbh,
                success: function(data) {
                    var json = data.split(',');
                    document.getElementById("<%=CM_CUSNAME.ClientID%>").value = json[0];
                    document.getElementById("<%=CM_EQUIP.ClientID%>").value = json[1];
                    document.getElementById("<%=CM_EQUIPMAP.ClientID%>").value = json[2];
                }
            });
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <asp:HiddenField ID="UserID" runat="server" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" Text="提 交" OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" CausesValidation="false" />
    </div>
    <table width="100%">
        <tr>
            <td style="width: 10px">
                <asp:Image ID="Image10" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                    onClick="switchGridVidew(this,'htjbxx')" Height="15" Width="15" runat="server" />
            </td>
            <td>
                售后服务基本信息
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server">
        <div id="htjbxx" style="display: block;">
            <table id="tab1" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                    <td style="width: 70px">
                        编号：
                    </td>
                    <td style="width: 484px">
                        <asp:TextBox ID="CM_BIANHAO" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="width: 113px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px">
                        合同号：
                    </td>
                    <td style="width: 484px">
                        <asp:TextBox ID="CM_CONTR" runat="server" Width="200px" onblur="autoHT()"></asp:TextBox>
                        <span id="span1" runat="server" visible="false" class="Error">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写合同编号"
                            ControlToValidate="CM_CONTR" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="CM_CONTR" ServicePath="~/Ajax.asmx"
                            ServiceMethod="SHHtCode" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                            EnableCaching="false">
                        </asp:AutoCompleteExtender>
                        <asp:Label runat="server" ID="lb" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="width: 113px">
                        顾客名称：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_CUSNAME" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px">
                        设备名称：
                    </td>
                    <td style="width: 484px">
                        <asp:TextBox ID="CM_EQUIP" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="width: 113px">
                        设备图号：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_EQUIPMAP" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px">
                        开始时间：
                    </td>
                    <td style="width: 484px">
                        <asp:TextBox ID="CM_STARTTIME" runat="server" class="easyui-datebox" editable="false" Width="200px"></asp:TextBox>
                    </td>
                    <td style="width: 113px">
                        结束时间：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_ENDTIME" runat="server" class="easyui-datebox" editable="false" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px">
                        服务内容概况：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_SERBASIC" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="width: 70px">
                        服务结果：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_RESULT" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px">
                        备注：
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="CM_NOTE" runat="server" TextMode="MultiLine" Width="400px" Height="70px"></asp:TextBox>
                    </td>
                    <td>
                        <span id="message" visible="false" class="Error" runat="server">删除确认前，请删除相应的登记信息及附件，以免造成数据库与服务器的压力！</span>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <asp:TabPanel ID="Tab_MYD" runat="server" TabIndex="0" HeaderText="客户服务调查表" Height="60px">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 83px">
                            <asp:Label ID="Label1" runat="server" Text="文件上传："></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="文件下载：" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                            <br />
                            <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                            <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid"
                                AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                Width="50%">
                                <Columns>
                                    <asp:BoundField DataField="showName" HeaderText="文件名称">
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
