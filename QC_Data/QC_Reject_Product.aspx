<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/BaseMaster.master"
    CodeBehind="QC_Reject_Product.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Reject_Product" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    不合格品通知单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
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

        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }

        //点击确定
        function savePick() {
            var r = Save();
            var orderId="";
             $("#<%=GV_RejectPro.ClientID %> input:checkbox:checked").each(function(){
           orderId+=  $(this).parent().parent().find("td:eq(1)").html()+",";
             
             });
            
            
          //  var orderId = $("#<%=GV_RejectPro.ClientID %> input:checkbox:checked:first").parent().parent().find("td:eq(1)").html();

            $.post("QC_AjaxHandler.aspx", { method: "PurAssign", Id: r.st_id, name: r.st_name, orderId: orderId }, function(data) {
                if (data == "success") { alert("保存成功"); window.location = "QC_Reject_Product.aspx"; }
                else { alert("保存失败，请联系管理员，或稍后操作"); }

            }, 'text');
            $('#win').dialog('close');
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box-wrapper">
                    <div style="height: 8px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <%--操作按钮--%>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_search" runat="server" Text="搜 索" OnClick="btn_search_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_reset" runat="server" Text="重 置" OnClick="btn_reset_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_View" runat="server" Text="查 看" OnClick="btn_View_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_Edit" runat="server" Text="编 辑" OnClick="btn_Edit_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_Del" runat="server" Text="删 除" OnClick="btn_Del_Click"
                                        OnClientClick="javascript:return confirm('确定删除吗？')" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_Print" runat="server" Text="打 印" OnClick="btn_Print_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_ChgNote" runat="server" Text="重 审" OnClick="btn_ChgNote_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_ExportExcel" runat="server" Text="导 出" OnClick="btn_ExportExcel_Click"
                                        OnClientClick="javascript:return confirm('提示：点击确定后准备导出，请稍等！\r\r弹出保存框前请不要重复点击导出')" />
                                    &nbsp;|&nbsp;<asp:Button ID="btnReject" runat="server" Text="驳回处理" OnClick="btnReject_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btnPurAssign" runat="server" Text="采购分工" OnClientClick="SelPersons()" />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="ddl_state" runat="server" OnSelectedIndexChanged="btn_search_Click"
                                        AutoPostBack="true" RepeatColumns="8">
                                        <asp:ListItem Text="全部" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="已审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="待处理" Value="3" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <asp:HyperLink ID="hyl_add" CssClass="hand" runat="server" NavigateUrl="~/QC_Data/QC_Reject_Product_Add.aspx?action=add">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />&nbsp;添
                                        加</asp:HyperLink>&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Pal_Query" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        单&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="tb_orderid" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        合同号：
                                        <asp:TextBox ID="tb_conId" runat="server" Text="" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        项目名称：
                                        <asp:TextBox ID="tb_pjinfo" runat="server" Text="" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        部件名称：<asp:TextBox ID="txt_BJMC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        图&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                                        <asp:TextBox ID="txtTH" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        抄送部门：<asp:DropDownList ID="dpl_CSBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                            <asp:ListItem Text="全部" Value="%"></asp:ListItem>
                                            <asp:ListItem Text="技术部" Value="03"></asp:ListItem>
                                            <asp:ListItem Text="质量部" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="生产部" Value="04"></asp:ListItem>
                                            <asp:ListItem Text="采购部" Value="05"></asp:ListItem>
                                            <asp:ListItem Text="无" Value="无"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        编 制 人：<asp:DropDownList ID="dpl_zdr" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        责任部门：<asp:TextBox ID="txtZRBM" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        处置方式：<asp:DropDownList ID="ddl_czfs" runat="server" OnSelectedIndexChanged="btn_search_Click"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="-全部-" Value="%"></asp:ListItem>
                                            <asp:ListItem Text="让步接收" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="返修" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="报废" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        是否验证：<asp:DropDownList ID="ddl_yz" runat="server" OnSelectedIndexChanged="btn_search_Click"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="-全部-" Value="%"></asp:ListItem>
                                            <asp:ListItem Text="已验证" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="未验证" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        制单时间：<asp:TextBox ID="sta_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>
                                        至&nbsp;<asp:TextBox ID="end_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="sta_time">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="calender_end" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="end_time">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        每页显示：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;条记录&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div style="overflow: auto">
                            <asp:GridView ID="GV_RejectPro" runat="server" CssClass="toptable grid" Width="100%"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GV_RejectPro_RowDataBound"
                                ShowFooter="true" EmptyDataText="没有符合搜索条件的数据" EmptyDataRowStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lb_bzr" runat="server" Text='<%#Eval("BZR") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lb_orderid" runat="server" Text='<%#Eval("Order_id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lb_state" runat="server" Text='<%#Eval("State") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblMPSTATE" runat="server" Text='<%#Eval("MPSTATE") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="单号" ItemStyle-Wrap="false" DataField="ORDER_ID" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="项目名称" ItemStyle-Wrap="false" DataField="PJ_NAME" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="合同号" ItemStyle-Wrap="false" DataField="CON_ID" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="部件名称" ItemStyle-Wrap="false" DataField="BJMC" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="图号" ItemStyle-Wrap="false" DataField="TH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="编制人" ItemStyle-Wrap="false" DataField="ST_NAME" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="责任部门" ItemStyle-Wrap="false" DataField="Duty_dep" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="评审人" ItemStyle-Wrap="false" DataField="PSR_NAME" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="不合格分级" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_rank" runat="server" Text='<%# Eval("Rank").ToString()=="0"?"一般不合格":Eval("Rank").ToString()=="1"?"严重不合格":"" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="处置方式" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_jsbresult" runat="server" Text='<%# Eval("CZFS").ToString()=="1"?"让步接收":Eval("CZFS").ToString()=="2"?"返修":Eval("CZFS").ToString()=="3"?"报废":"" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="审批人" ItemStyle-Wrap="false" DataField="SPR_NAME" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="审批结果" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_result" runat="server" Text='<%# Eval("SPR_RESULT").ToString()=="0"?"同意":Eval("SPR_RESULT").ToString()=="1"?"不同意":"" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否验证" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_yz" runat="server" Text='<%# Eval("STATE").ToString()=="5"?"是":"否" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="制单时间" ItemStyle-Wrap="false" DataField="Inform_time"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="采购责任人" ItemStyle-Wrap="false" DataField="PurManNM" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Lbtn_review" runat="server" ForeColor="Red" CommandArgument='<%#Eval("Order_id") %>'
                                                OnClick="Lbtn_review_OnClick" CommandName="audit">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icons/shenhe.gif" />
                                            </asp:LinkButton>
                                            <input type="hidden" runat="server" id="hidState" value='<%#Eval("State") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#ffffff" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            </asp:GridView>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                        </div>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                    <!--box-outer END -->
                </div>
                <!--box-wrapper END -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_ExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="05">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
    </div>

    <script type="text/javascript">
        //单击行变色
        function RowClick(obj) {
            //判断是否单击的已选择的行，如果是则取消该行选择
            if (obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked == false) {
                obj.style.backgroundColor = '#ffffff';
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
            }
            else {
                var table = obj.parentNode.parentNode;
                var tr = table.getElementsByTagName("tr");

                var ss = tr.length;
                for (var i = 1; i < ss - 1; i++) {
                    tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                }
                obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
            }
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
