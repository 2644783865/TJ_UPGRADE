<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="MainPlanProView.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.MainPlanProView" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    项目计划管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/jquery/ajaxfileupload.js" type="text/javascript"></script>

    <script type="text/javascript">

        function ajaxFileUpload() {
            //            $("#loading").ajaxStart(function() {
            //                $(this).show();
            //            }).ajaxComplete(function() { $(this).hide(); });
            $.ajaxFileUpload({
                url: 'MainPlan_AjaxHandler.aspx?method=UploadPLFiles',
                secureuri: false,
                fileElementId: 'fileToUpload',
                dataType: 'json',
                success: function(data, status) {
                    ControlResult(data);
                },
                error: function(data, status, e) {
                    alert("请选择文件！");
                }
            })
            return false;
        }

        function ControlResult(data) {
            $("#lblUpLoadResult").html(data.msg);
            if (data.msg.indexOf('成功') > -1) {
                $("#trFileName").empty();
                $("#trFileName").html("<a href='#'>" + data.filename + "</a>");
                $("#hidGuanLianTime").val(data.guanlianTime);
            }
            else {
                $("#trFileName").empty();
            }
        }
     
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>中材(天津)重型机械有限公司项目计划管理</b>
                        </td>
                        <td class="tdleft1">
                            上传附件:
                        </td>
                        <td>
                            <input id="fileToUpload" type="file" name="fileToUpload" style="width: 150px" />
                            <input type="button" id="buttonUpload" onclick="return ajaxFileUpload();" value="上传附件" />
                            <label id="lblUpLoadResult" visible="false" style="color: Red">
                            </label>
                            <input type="hidden" runat="server" id="hidGuanLianTime" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" OnClientClick="return confirm('您确定要删除吗？')" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss" />
                            <input type="hidden" id="hidId" value='<%#Eval("Id")%>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ST_NAME" HeaderText="上传人员" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fileUpDate" HeaderText="上传时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="附 件" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="download" CssClass="link" runat="server" CausesValidation="False"
                                OnClick="download_OnClick" CommandArgument='<%#Eval("Id")%>'>
                                           <%#Eval("fileName")%>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging" runat="server" />
        </div>
    </div>
</asp:Content>
