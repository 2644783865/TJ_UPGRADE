<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_MP_Require_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Require_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划审批
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }

        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
        //双击查看明细
        function Dblclik_ShowDetail(tracknum) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_MP_Require_Audit_Detail.aspx?NoUse=" + time + "&tracknum_table=" + encodeURIComponent(tracknum), '', "dialogHeight:400px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

        function Submit() {
            var ok = confirm("提交后不能修改，确认提交吗？");
            if (ok == true) {
                if (document.getElementById("<%=btnsubmit.ClientID%>") == null) {
                    alert("您无权提交！！！");
                    return false;
                }
                else {
                    document.getElementById("<%=btnsubmit.ClientID%>").click();
                    document.getElementById("btnMpsubmit").disabled = true;
                }
            }
            return ok;
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0" AutoPostBack="false">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="材料需用单" TabIndex="0">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div style="height: 6px" class="box_top">
                            </div>
                            <div class="box-outer">
                                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr>
                                        <td style="font-size: large; text-align: center;" colspan="7">
                                            材料需用计划&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkiffast" runat="server" Enabled="false" />是否加急物料
                                        </td>
                                        <td align="right">
                                            <asp:Image ID="Image3" ToolTip="返回上一页" CssClass="hand" Height="16" Width="16" runat="server"
                                                onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" align="right">
                                            任务号:
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="tsa_id" runat="server" Width="100%" />
                                        </td>
                                        <td style="width: 8%" align="right">
                                            项目名称:
                                        </td>
                                        <td style="width: 14%">
                                            <asp:Label ID="lab_proname" runat="server" Width="100%" />
                                        </td>
                                        <input id="proid" type="text" runat="server" readonly="readonly" style="display: none" />
                                        <td style="width: 8%" align="right">
                                            设备名称:
                                        </td>
                                        <td style="width: 14%">
                                            <asp:Label ID="lab_engname" runat="server" Width="100%" />
                                        </td>
                                        <td style="width: 8%" align="right">
                                            计划编号:
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Label ID="mp_no" runat="server" Width="100%" />
                                        </td>
                                        <input id="eng_type" type="text" runat="server" readonly="readonly" value="" style="display: none" />
                                        <input id="txtPlanType" type="text" runat="server" readonly="readonly" value="" style="display: none" />
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" align="right">
                                            批准日期:
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_approval" runat="server" Width="100%" />
                                        </td>
                                        <td style="width: 8%" align="right">
                                            图号:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMap" runat="server" Width="100%" />
                                        </td>
                                        <td style="width: 8%" align="right">
                                            编制日期:
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="txt_plandate" runat="server" Width="100%" />
                                        </td>
                                        <td align="center">
                                            <asp:HyperLink ID="hpView" ToolTip="本批变更引起的新增/减少计划量" CssClass="link" Target="_blank"
                                                runat="server">
                                                查看变更信息
                                                <asp:Image ID="ImageTo" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" /></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblNote" Width="80" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtBZ" Width="300px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="left" colspan="4" valign="middle">
                                            查询类别:<asp:DropDownList ID="ddlQueryType" runat="server">
                                                <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                <asp:ListItem Text="物料编码" Value="MP_MARID"></asp:ListItem>
                                                <asp:ListItem Text="物料名称" Value="MP_NAME"></asp:ListItem>
                                                <asp:ListItem Text="物料规格" Value="MP_GUIGE"></asp:ListItem>
                                                <asp:ListItem Text="材质" Value="MP_CAIZHI"></asp:ListItem>
                                                <asp:ListItem Text="计划跟踪号" Value="MP_TRACKNUM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtQueryText" runat="server"></asp:TextBox>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                                            <asp:Button ID="btnOrderStateShow" runat="server" Width="80" Text="显示订单状态" OnClick="btnOrderStateShow_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <asp:Label ID="lblAfter" runat="server" ForeColor="Red" Visible="false" Font-Bold="true"
                                    Text="本批变更新增或减少计划量:"></asp:Label>
                                <yyc:SmartGridView ID="GridView2" Width="100%" CssClass="toptable grid" Visible="false"
                                    runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                                    <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_MARID" HeaderText="材料ID" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField HeaderText="长度(mm)" DataField="MP_LENGTH" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField HeaderText="宽度(mm)" DataField="MP_WIDTH" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MP_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量" HeaderStyle-Wrap="false" DataFormatString="{0:f}"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" Visible="false" />
                                        <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_MASHAPE" HeaderText="材料类别" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    </Columns>
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <FixRowColumn FixRowType="Header,Pager" TableHeight="150px" TableWidth="100%" />
                                </yyc:SmartGridView>
                                <br />
                                <asp:Label ID="lblBefore" runat="server" ForeColor="Red" Visible="false" Font-Bold="true"
                                    Text="变更后的物料计划:"></asp:Label>
                                <asp:Panel ID="NoDataPanel" Width="100%" Visible="false" runat="server">
                                    <div style="text-align: center; font-size: medium;">
                                        <br />
                                        没有记录!</div>
                                </asp:Panel>
                                <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                <input type="hidden" id="state" runat="server" value='<%# Eval("MP_STATE") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_MARID" HeaderText="材料ID" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="到货情况" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hplAOG" CssClass="hand" runat="server">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/icons/dindan.jpeg" Height="16" Width="16"
                                                        border="0" hspace="2" align="absmiddle" runat="server" />
                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                </asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" DynamicServiceMethod="GetAOGDynamicContent"
                                                    DynamicContextKey='<%# Eval("MP_TRACKNUM") %>' DynamicControlID="Panel1" TargetControlID="hplAOG"
                                                    PopupControlID="Panel1" Position="Right" OffsetY="25">
                                                </cc1:PopupControlExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField HeaderText="长度(mm)" DataField="MP_LENGTH" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField HeaderText="宽度(mm)" DataField="MP_WIDTH" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MP_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_WEIGHT" DataFormatString="{0:F2}" HeaderText="重量(kg)"
                                            HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_MASHAPE" HeaderText="材料类别" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_KU" HeaderText="库" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("MP_MASHAPE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle Wrap="false" />
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <FixRowColumn FixRowType="Header,Pager" TableHeight="400px" TableWidth="100%" FixColumns="0" />
                                </yyc:SmartGridView>
                                <asp:Panel ID="Panel1" runat="server">
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div style="height: 6px" class="box_top">
                            </div>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;
                                            <input id="btnMpsubmit" type="button" value="提 交" onclick="return Submit();" />
                                            <asp:Button ID="btnsubmit" CssClass="hidden" runat="server" OnClick="btnsubmit_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="ckbMessage" Checked="true" runat="server" />&nbsp;邮件提醒
                                        </td>
                                        <td align="right">
                                            <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                            材料需用计划
                                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr>
                                        <td style="width: 10%" align="center">
                                            项目名称
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Label ID="proname" runat="server" Width="100%" />
                                        </td>
                                        <td style="width: 10%" align="center">
                                            设备名称
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Label ID="engname" runat="server" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            编制
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label>
                                        </td>
                                        <input id="editorid" type="text" runat="server" readonly="readonly" style="display: none" />
                                        <%--<input id="depid" type="text" runat="server" readonly="readonly" style="display:none" />
                                <input id="depname" type="text" runat="server" readonly="readonly" style="display:none" />--%>
                                        <td align="center">
                                            编制日期
                                        </td>
                                        <td>
                                            <asp:Label ID="plandate" runat="server" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            主管审核
                                        </td>
                                        <td colspan="3">
                                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()"
                                                            Visible="false">
                                                            <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rblfirst_OnSelectedIndexChanged">
                                                            <asp:ListItem Text="同意" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            部门领导
                                        </td>
                                        <td colspan="3">
                                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rblsecond_OnSelectedIndexChanged">
                                                            <asp:ListItem Text="同意" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="5"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            主管经理
                                        </td>
                                        <td colspan="3">
                                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                                            <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px"
                                                            OnSelectedIndexChanged="rblthird_OnSelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="同意" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="7"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <div>
        <div>
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
                                <input id="dep" name="dept" value="03">
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
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
