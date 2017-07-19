﻿<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_GdzcBaofei_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcBaofei_Detail"
    Title="固定资产报废" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产报废
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
 
//        function rblSPJB_onchange() {
//            $("#td_SPLX input:radio:checked").each(function() {
//                if ($(this).val() == "1") {
//                    $("#<%=SPR1.ClientID%>").show();
//                    $("#<%=SPR2.ClientID%>").hide();
//                }
//                else if ($(this).val() == "2") {
//                    $("#<%=SPR1.ClientID%>").show();
//                    $("#<%=SPR2.ClientID%>").show();
//                }
//            });
//        }

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtSPR1.ClientID %>").val(r.st_name);
                $("#<%=hidSPR1ID.ClientID %>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=txtSPR2.ClientID %>").val(r.st_name);
                $("#<%=hidSPR2ID.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        function btnSave_OnClientClick() {
            var a = true;
//            $("#td_SPLX input:radio:checked").each(function() {
//                if ($(this).val() == "1") {
//                  if($("#rblSPJB").attr("disabled")==false){
//                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "") {
//                        alert("请选择审批人后再提交审批!!!");
//                        a = false;
//                    }
//                  }
//                }
//                else if ($(this).val() == "2") {
//                 if($("#rblSPJB").attr("disabled")==false){
//                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2ID.ClientID %>").val() == "") {
//                        alert("请选择审批人后再提交审批!!!");
//                        a = false;
//                    }
//                  }
//                }
//            });
            return a;
        }
          function returnval() {
            var hid = document.getElementById('<%=Hidden.ClientID %>');
            return hid.value;
        }
              function autoHeight() {
            var iframe = document.getElementById("filework");
            if (iframe.Document) {//ie自有属性
                iframe.style.height = iframe.Document.documentElement.scrollHeight;
            }
            else if (iframe.contentDocument) {//ie,firefox,chrome,opera,safari
                iframe.height = iframe.contentDocument.body.offsetHeight;
            }
        }
              function addEvent(eventName, element, fn) {
            if (element.attachEvent) element.attachEvent("on" + eventName, fn);
            else element.addEventListener(eventName, fn, false);
        }
                window.onload = function() {
            var td = document.getElementById("mytd");
            var id = document.getElementById('<%=Hidden.ClientID %>').value;
            var iframeA = document.createElement('iframe');
            iframeA.setAttribute("src", "gdzcbaofeidownload.aspx?id=" + id);
            iframeA.setAttribute("id", "filework");
            iframeA.setAttribute("width", "100%");
            iframeA.setAttribute("marginwidth", "0");
            iframeA.setAttribute("frameborder", "0");
            iframeA.setAttribute("scrolling", "no");
            addEvent("load", iframeA, autoHeight);
            td.appendChild(iframeA);
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="Hidden" runat="server" />
    <table width="100%" style="text-align: center">
        <tr align="right">
            <td>
                <asp:Button runat="server" ID="btnSave" Visible="false" Text="保 存" OnClientClick="return btnSave_OnClientClick()"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="提 交" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="返 回" OnClick="btnBack_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="固定资产报废明细" Width="100%" TabIndex="0">
            <HeaderTemplate>
                固定资产报废明细
            </HeaderTemplate>
            <ContentTemplate>
                <div id="detail" style="width: 100%" align="center">
                    <asp:Panel ID="panDetail" runat="server" Width="98.6%" Enabled="False">
                        <table id="table1" width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 审批编号：
                                    <asp:TextBox ID="txtSpbh" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <div style="width: 100%; overflow: auto">
                                    <table width="100%" id="tab" align="center" cellpadding="2" cellspacing="1" class=" grid "
                                        border="1">
                                        <asp:Repeater ID="rptGDZCBF" runat="server">
                                            <HeaderTemplate>
                                                <tr style="background-color: #B9D3EE; height: 30px;">
                                                    <td align="center">
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>固定资产编号</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>类型</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>规格或参数</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>使用人</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>使用部门</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>开始时间</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>使用年限(月)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>价值(元)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>存放地点</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>备注</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                    onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                                                    style="height: 30px">
                                                    <td align="center">
                                                        <%--<asp:CheckBox ID="cbxXuHao" runat="server" Text="<%#(Container.ItemIndex + 1)%>" />--%><%#(Container.ItemIndex + 1)%>
                                                    </td>
                                                    <td id="td_BIANHAO" runat="server" align="center">
                                                        <%#Eval("BIANHAO")%>
                                                    </td>
                                                    <td id="td_NAME" runat="server" align="center">
                                                        <%#Eval("NAME")%>
                                                    </td>
                                                    <td id="td_TYPE" runat="server" align="center">
                                                        <%#Eval("TYPE")%>
                                                    </td>
                                                    <td id="td_MODEL" runat="server" align="center">
                                                        <%#Eval("MODEL")%>
                                                    </td>
                                                    <td id="td_SYR" runat="server" align="center">
                                                        <%#Eval("SYR")%>
                                                    </td>
                                                    <td id="td_SYBUMEN" runat="server" align="center">
                                                        <%#Eval("SYBUMEN")%>
                                                    </td>
                                                    <td id="td_SYDATE" runat="server" align="center">
                                                        <%#Eval("SYDATE")%>
                                                    </td>
                                                    <td id="td_NX" runat="server" align="center">
                                                        <%#Eval("NX")%>
                                                    </td>
                                                    <td id="td_JIAZHI" runat="server" align="center">
                                                        <%#Eval("JIAZHI")%>
                                                    </td>
                                                    <td id="td_PLACE" runat="server" align="center">
                                                        <%#Eval("PLACE")%>
                                                    </td>
                                                    <td id="td_NOTE" runat="server" align="center">
                                                        <%#Eval("NOTE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lbTiXing" runat="server" Text="*已提交报废审批且仍未完成审批的项自动取消勾选" ForeColor="Red"></asp:Label>
                    </asp:Panel>
                </div>
                <tr id="wenjian" runat="server">
                    <td>
                        附件上传：
                    </td>
                    <td style="text-align: center;">
                        <div id="mytd">
                        </div>
                    </td>
                </tr>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="1" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div id="shenhe" class="box-wrapper">
                    <asp:Panel ID="panShenhe" runat="server" Enabled="false">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td id="td_SPLX" align="right">
                                        <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Enabled="false" AutoPostBack="true" OnTextChanged="rblSPJB_onchange">
                                            <asp:ListItem Text="一级审批" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        固定资产报废审批
                                        <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                            Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:HiddenField runat="server" ID="hidZDRID" />
                                <asp:HiddenField runat="server" ID="hidSPR1ID" />
                                <asp:HiddenField runat="server" ID="hidSPR2ID" />
                                <tr>
                                    <td align="center">
                                        制单人
                                    </td>
                                    <td>
                                        <asp:Label ID="lbZDR" runat="server" Width="100%"></asp:Label>
                                    </td>
                                    <td align="center">
                                        制单时间
                                    </td>
                                    <td>
                                        <asp:Label ID="lbZD_SJ" runat="server" Width="40%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        制单人建议：
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtZDR_JY" Text="" TextMode="MultiLine" Width="100%"
                                            Font-Size="Medium" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="SPR1" runat="server">
                                    <td align="center">
                                        一级审核
                                    </td>
                                    <td colspan="3">
                                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr>
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtSPR1" runat="server" onfocus="this.blur()" Width="80px"></asp:TextBox>
                                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="xr1()" Visible="false">
                                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList runat="server" ID="rblSPR1_JL" RepeatDirection="Horizontal"
                                                        Style="margin: auto" Width="80%" RepeatColumns="2" Enabled="false">
                                                        <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lbSPR1_SJ" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtSPR1_JY" runat="server" Height="42px" TextMode="MultiLine" Width="100%"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="SPR2" runat="server">
                                    <td align="center">
                                        二级审核
                                    </td>
                                    <td colspan="3">
                                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr>
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtSPR2" runat="server" Width="80px" onfocus="this.blur()"></asp:TextBox>
                                                    <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="xr2()" Visible="false">
                                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList runat="server" ID="rblSPR2_JL" RepeatDirection="Horizontal"
                                                        Style="margin: auto" Width="80%" RepeatColumns="2" Enabled="false">
                                                        <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lbSPR2_SJ" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtSPR2_JY" runat="server" TextMode="MultiLine" Width="100%" Height="42px"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
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
                                <input id="dep" name="dept" />
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
