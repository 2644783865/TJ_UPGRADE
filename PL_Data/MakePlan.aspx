<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="MakePlan.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.MakePlan" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制定主生产计划
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/jquery/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CalculateDate(input) {
            var index = $(input).parent().parent().find("span:first").html();
            //            console.log($($("#ctl00_PrimaryContent_GridView1 tr")[index]).find("input:first"));
            //  console.log($($("#ctl00_PrimaryContent_GridView1 tr")[index]).find("input"));

            //取得计划天数
            var days = $($("#ctl00_PrimaryContent_GridView1 tr")[index]).find("input:first").val();
            //取得开始日期
            var startdate = $($("#ctl00_PrimaryContent_GridView1 tr")[index]).find("input:eq(3)").val();
            var pattemNum = /^\d{1,2}$/;
            var pattemDate = /\d{4}\-\d{2}\-\d{2}/;
            if (pattemNum.test(days) && pattemDate.test(startdate)) {
                var enddate = AddDays(startdate, days);
                //将计算后的日期输入截止日期文本框
                //.datebox('setValue',enddate);
                $($("#ctl00_PrimaryContent_GridView1 tr")[index]).find(".easyui-datebox:last").datebox('setValue', enddate);
            }

        }

        //计算日期函数
        function AddDays(beginDate, days) {
            var start = beginDate.replace(/-/g, "/"); //将日期转换为"/"相连
            var begin = new Date(start);
            var newtimems = begin.getTime() + (days * 24 * 60 * 60 * 1000);
            begin.setTime(newtimems);
            var month = parseInt(begin.getMonth()) + 1
            var end = begin.getFullYear() + "-" + month + "-" + begin.getDate();
            return end;
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 15%">
                            任务号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            合同号:<asp:Label ID="lab_contract" runat="server"></asp:Label>
                            <input id="con_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                        <td style="width: 18%">
                            项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                            <input id="pro_name" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                        <td style="width: 18%">
                            设备名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lbnSave" OnClick="lbnSave_Click">
                                保存
                                <asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" /></asp:LinkButton>
                        </td>
                        <td align="right">
                            <%--  <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            <asp:LinkButton ID="lbnNew" runat="server" CssClass="link" OnClientClick="return openLink(0)">选择现有模板</asp:LinkButton>--%>
                            现有模板:
                            <asp:DropDownList ID="ddlModel" runat="server" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
        border="1">
        <tr>
            <td class="tdleft1">
                交货时间:
            </td>
            <td class="tdright1">
                <asp:TextBox ID="txtDelivery" runat="server" class="easyui-datebox"></asp:TextBox>
                &nbsp;&nbsp;
            </td>
            <td class="tdleft1">
                实际交货时间:
            </td>
            <td class="tdright1">
                <asp:TextBox ID="txtDeliveryActural" runat="server" class="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
       
    </table>
    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
        CellPadding="2" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <%-- <asp:HiddenField ID="HIDXUHAO" Value='<%#Eval("TARGET_ID")%>' runat="server" />--%>
                </ItemTemplate>
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:BoundField DataField="MP_TYPE" HeaderText="计划类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MP_PLNAME" HeaderText="主计划项" HeaderStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="计划天数" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px">
                <ItemTemplate>
                    <input type="text" runat="server" value='<%# Eval("MP_DAYS") %>' id="txtDays" style="width: 40px"
                        onchange="CalculateDate(this)" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="预警天数" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px">
                <ItemTemplate>
                    <input type="text" runat="server" value='<%# Eval("MP_WARNINGDAYS") %>' id="txtWarningDays"
                        style="width: 40px" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划开始日期" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <input id="txtStart" type="text" runat="server" value='<%# Eval("MP_STARTDATE") %>'
                        style="width: 100px" class="easyui-datebox" data-options="onSelect:function(){CalculateDate(this);}" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划截止日期" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <input id="txtFinish" type="text" runat="server" value='<%# Eval("MP_ENDTIME") %>'
                        style="width: 100px" class="easyui-datebox" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="该项状态" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("MP_STATE").ToString()=="0"?"未开始":Eval("MP_STATE").ToString()=="1"?"进行中...":Eval("MP_STATE").ToString()=="2"?"完成":"未知错误" %>'></asp:Label>
                    <input type="hidden" runat="server" value='<%# Eval("MP_STATE") %>' id="hidState" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际完成时间" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblActural" runat="server" Text='<%# Eval("MP_ACTURALTIME") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备 注" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="160">
                <ItemTemplate>
                    <input id="txtNote" type="text" runat="server" value='<%# Eval("MP_NOTE") %>' style="width: 200px" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn TableHeight="100%" TableWidth="100%" />
    </yyc:SmartGridView>
</asp:Content>
