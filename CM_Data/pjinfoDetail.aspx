<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"
    AutoEventWireup="true" CodeBehind="pjinfoDetail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.pjinfoDetail_new" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        /*合同拆分增加*/
        function proadd() {
            var ddlid = document.getElementById('<%=DDL_NAME.ClientID%>').value;
            if (ddlid == "00") {
                alert("请选择合同！");
                return false;
            }
        }
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (pattem.test(testnum)) {
                if (parseInt(testnum) > 10) {
                    alert("请输入的数值小于10,请重新输入！！！");
                    obj.value = "1";
                }
            }
            else {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg"
        runat="server" />
    <div class="RightContentTitle">
        修改或新建任务号信息</div>
    <div class="box-inner">
        <div class="box_right">
            <div class='box-title'>
                <table width='100%'>
                    <tr>
                        <td>
                            合同信息(带<span class="red">*</span>号的为必填项)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                    <td style="width: 150px">
                        选择合同
                    </td>
                    <td style="width: 250px">
                        <asp:DropDownList ID="DDL_NAME" runat="server" Width="150px">
                        </asp:DropDownList>
                        <span class="red">*</span>
                    </td>
                    <td style="width: 150px">
                        增加任务号数目
                    </td>
                    <td>
                        <input id="num" runat="server" value="1" type="text" style="width: 50px" onblur="CheckNum(this);" />&nbsp;&nbsp;
                        <asp:Button ID="btnadd" runat="server" Text="增 加" OnClientClick="proadd()" OnClick="btnadd_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        添加人
                    </td>
                    <td style="width: 250px">
                        <asp:Label ID="tb_PJ_MANCLERK" runat="server"></asp:Label>
                    </td>
                    <td>
                        添加时间
                    </td>
                    <td>
                        <asp:Label ID="addtime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        备注：
                    </td>
                    <td>
                        <asp:TextBox ID="ta_PJ_NOTE" runat="server" TextMode="MultiLine" Width="300px" Height="100px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" >
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Width="10px" CssClass="checkBoxCss" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程简称" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ComboBox ID="engid" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" 
                                                    DropDownStyle="DropDownList" Height="15px" DataValueField='<%#DataBinder.Eval(Container.DataItem, "ENG_ID") %>'
                                                    Width="90px" OnSelectedIndexChanged="engid_SelectedIndexChanged">
                                                </asp:ComboBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="engname" runat="server" style="border-style: none; height: 21px" type="text"
                                                    value='<%# Eval("TSA_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程量(kg)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="engtotal" runat="server" style="border-style: none; height: 21px" type="text"
                                                    value='<%# Eval("TSA_TOTALWGHT") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ENG_STRTYPE" HeaderText="工程类型">
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="台数" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="number" runat="server" style="border-style: none; width: 50px; height: 21px"
                                                    type="text" value='<%# Eval("TSA_NUMBER") %>' onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:Panel ID="NoDataPanel" runat="server">
                                    没有记录!</asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="delete" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label>
            <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
            <br /><br />
            <asp:Button ID="btn_Submit" runat="server" Text="提交" OnClick="btnConfirm_Click" />
            &nbsp;
            <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
        </div>
    </div>
</asp:Content>
