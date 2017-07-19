<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_Task_Manage.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Task_Manage" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    质量任务分工表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/jquery/jquery-1.4.2-vsdoc.js" type="text/javascript"></script>

    <script type="text/javascript">

        function openLink(url) {
            var returnVlue = window.showModalDialog("QC_Task_Assign.aspx?Id=" + url, '', "dialogHeight:500px;dialogWidth:800px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlSearch.ClientID%>');
            var acNameClientId = "<%=aceSearch.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }

        $(function() {
            //全选
            $("#<%=chxCheckAll.ClientID  %>").click(function() {
                var checkbox = $("#<%=GridView1.ClientID %> input[type=checkbox]");
                console.info($(this).attr("checked"));
                if ($(this).attr("checked") == true) {
                    $(checkbox).attr("checked", true);
                }
                else {
                    $(checkbox).attr("checked", false);
                }
            });
        });
        $(function() {
            //连选
            $("#btnSelect").click(function() {
                var start;
                var end;
                $("#<%=GridView1.ClientID %> input[type=checkbox]").each(function() {
                    if ($(this).attr("checked") == true) {
                        if (start == null) {
                            start = $(this).parent().parent().find("#lblIndex").val();
                        }
                        end = $(this).parent().parent().find("#lblIndex").val();

                    }
                });

                if ($("#<%=GridView1.ClientID %> input[type=checkbox]:checked").length >= 2) {
                    var sta = parseInt(parseInt(start) - 1);
                    var en = parseInt(parseInt(end) - 1);
                    $("#<%=GridView1.ClientID %> input[type=checkbox]:gt(" + sta + "):lt(" + en + ")").attr("checked", true);
                }
                else {
                    alert("请选择两条以上的数据进行连选！！！");
                }


            });


        });
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 11%; height: 24px">
                            全选/取消<input id="chxCheckAll" runat="server" type="checkbox" />&nbsp;
                            <input type="button" id="btnSelect" title="选择起止项后点击连选,起止项间所有记录选中" value="连选" />
                        </td>
                        <td>
                            按类查询:<asp:DropDownList ID="ddlSearch" runat="server" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="CM_PROJ"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="TSA_ENGNAME"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="TSA_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" onkeydown="return OnTxtPersonInfoKeyDown()"
                                Width="120px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="aceSearch" runat="server" CompletionSetCount="15" ServiceMethod="GetSuggestions_Tech"
                                FirstRowSelected="true" Enabled="true" DelimiterCharacters="" ServicePath="~/TM_Data/TM_WebService.asmx"
                                TargetControlID="txtSearch" UseContextKey="True" CompletionInterval="10" MinimumPrefixLength="1">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " OnClick="btnSearch_Click" />
                        </td>
                        <td align="left">
                            质检员：<asp:DropDownList ID="drp_zjy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            任务状态:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="rblstatus" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="待分工" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已分工" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已作废" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            排序:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="任务号" Value="TSA_ID" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                                <asp:ListItem Text="质检员" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbnPlentyAssign" runat="server" OnClick="lbnPlentyAssign_Click" >
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server"  />
                                批量分工</asp:LinkButton>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                            <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                Position="Left" Enabled="true" runat="server" OffsetX="-400" OffsetY="-25" TargetControlID="HyperLink1"
                                PopupControlID="palORG">
                            </cc1:PopupControlExtender>
                            <asp:Panel ID="palORG" Width="50%" Style="display: none; visibility: hidden; border-style: solid;
                                border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                            title="关闭">X</a>
                                                    </div>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="btnSearch_Click"
                                                        Text="查 询" />
                                                    &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick"
                                                        Text="清 空" />&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 98%">
                                                    <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskidView"
                                                        QueryRows="4" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;" id="tableDiv">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        OnRowDataBound="GridView1_OnRowDataBound" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("TSA_ID") %>'></asp:Label>
                                    <input type="hidden" runat="server" id="hidQSAID" value='<%# Eval("QSA_ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input type="checkbox" runat="server" id="cbx" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="lblIndex"
                                readonly="readonly" style="border-style: none; width: 20px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                            <div style="padding-bottom: 1px;">
                            </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:BoundField DataField="TSA_ID" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_CONTYPE" HeaderText="设备类型" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="QSA_TCCHGERNM" HeaderText="任务分工人" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="QSA_QCCLERKNM" HeaderText="质检员" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                         
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("QSA_STATE").ToString()=="0"?"待分工":Eval("QSA_STATE").ToString()=="1"?"已分工...":Eval("QSA_STATE").ToString()=="2"?"已作废":"未知错误" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="分工信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask" CssClass="link" runat="server" NavigateUrl='<%# "~/QC_Data/QC_Task_Assign.aspx?Id="+Eval("QSA_ID")%>'>
                                        <asp:Image ID="InfoImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        任务分工
                                    </asp:HyperLink>
                                    <asp:HyperLink ID="hl_re_task" CssClass="link" Visible="false" runat="server">
                                        <asp:Image ID="taskImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        重新分工
                                    </asp:HyperLink>
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
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblstatus" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSort" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
