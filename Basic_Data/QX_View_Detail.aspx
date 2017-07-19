<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QX_View_Detail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.QX_View_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#tab input[name*=CKBOX_SELECT]").click(function() {

                //  console.log($(this).attr("check"));
                if ($(this).attr("checked")) {
                   // alert("aa");
                   // console.log($(this).parent().parent());
                    $(this).parent().parent().parent().find("input[type=checkbox]").attr("checked", true);
                }
                else {
                    $(this).parent().parent().parent().find("input[type=checkbox]").attr("checked", false);
                }


            });

        })

    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td align="right" width="100px">
                            姓名：
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="hidSTID" />
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="保存权限" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    全选
                                </td>
                                <td>
                                    序号
                                </td>
                                <td>
                                    类型
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    权限配置
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" name="CKBOX_SELECT" CssClass="checkBoxCss" runat="server"
                                        Checked="false" />
                                </td>
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("pType")%>'></asp:Label>
                                    <input type="hidden" runat="server" id="pId" value='<%#Eval("pId") %>' />
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="lblPosition" runat="server" Text='<%#Eval("pName")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblDep" runat="server" RepeatColumns="14">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
