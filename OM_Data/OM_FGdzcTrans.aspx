<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_FGdzcTrans.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FGdzcTrans" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    非固定资产转移&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tab
        {
            width: 70%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
    </style>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#<%=panSPR1.ClientID%>").hide();
            $("#<%=panSPR2.ClientID%>").hide();
            $("#<%=panSPR3.ClientID%>").hide();
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    //                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR1.ClientID%>").val("蔡伟疆");
                    //                    }
                    //                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR2.ClientID%>").val("蔡伟疆");
                    //                    }
                }
                else if ($(this).val() == "3") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").show();
                }
            });
        })

        function rblSPJB_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").hide();
                    $("#<%=panSPR3.ClientID%>").hide();
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").hide();
                    //                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR1.ClientID%>").val("蔡伟疆");
                    //                    }
                    //                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR2.ClientID%>").val("周文轶");
                    //                    }
                }
                else if ($(this).val() == "3") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").show();
                }
            });
        }

    </script>

    <script type="text/javascript" language="javascript">
        $(function() {
            //console.log($("select[name*=ddl_bumen]"));
            $("select[name*=ddl_bumen2]").change(function() {
                $ddlDep = $(this);
                var depId = $(this).val();
                $.ajax({
                    url: 'OM_AjaxHandler.aspx',
                    type: 'post',
                    data: { method: 'FindDepPeo', Id: depId },
                    dataType: 'json',
                    success: function(data) {
                        //console.log($ddlDep.parent().parent().find("select[name*=ddl_user]"));
                        var $ddlUser = $ddlDep.parent().parent().find("select[name*=ddl_user2]");
                        $ddlUser.empty();
                        for (i = 0; i < data.length; i++) {
                            var option = $("<option ></option>").val(data[i].st_id).html(data[i].st_name);
                            $ddlUser.append(option);

                        }
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyr2]").val($ddlUser.find(":selected").text());
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyrId2]").val($ddlUser.val());

                    }
                });
            });
        });
        $(function() {
            $("select[name*=ddl_user2]").change(function() {
                $(this).parent().find("input[type=hidden][name*=hidSyr2]").val($(this).find(":selected").text());
                $(this).parent().find("input[type=hidden][name*=hidSyrId2]").val($(this).val());
                // console.log($("input[type=hidden][name*=hidSyr]").val());
                //console.log($("input[type=hidden][name*=hidSyrId]").val());
            });
        });

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function xr3() {
            $("#hidPerson").val("person3");
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
            if (id == "person3") {
                $("#<%=txtSPR3.ClientID %>").val(r.st_name);
                $("#<%=hidSPR3ID.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        function btnSubmit_OnClientClick() {
            var a = true;
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "") {
                        alert("请选择审批人后再提交审批!!!");
                        a = false;
                    }
                }
                else if ($(this).val() == "2") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2ID.ClientID %>").val() == "") {
                        alert("请选择审批人后再提交审批!!!");
                        a = false;
                    }
                }
                else if ($(this).val() == "3") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2ID.ClientID %>").val() == "" || $("#<%=txtSPR3.ClientID %>").val() == "" || $("#<%=hidSPR3ID.ClientID %>").val() == "") {
                        alert("请选择审批人后再提交审批!!!");
                        a = false;
                    }
                }
            });
            return a;
        }
            
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btn_submit" Text="提 交" OnClientClick="return btnSubmit_OnClientClick()"
                                Width="40px" Height="25px" BackColor="LightGreen" OnClick="btn_submit_click"
                                runat="server" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button runat="server" ID="btnBack" Text="返 回" OnClick="btnBack_OnClick" Width="40px"
                                Height="25px" BackColor="LightGreen" />
                            &nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer">
        <asp:TabPanel runat="server" ID="TabPanel" HeaderText="基本信息">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="width: 100%; overflow: auto">
                            <asp:Panel runat="server" ID="panJBXX">
                                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <td>
                                                    <strong>非固定资产转移单</strong>
                                                </td>
                                            </tr>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    <strong>序号</strong>
                                                </td>
                                                <td>
                                                    <strong>编号</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                    <strong>规格或参数</strong>
                                                </td>
                                                <td>
                                                    <strong>转出人</strong>
                                                </td>
                                                <td>
                                                    <strong>转出部门</strong>
                                                </td>
                                                <td>
                                                    <strong>转出地点</strong>
                                                </td>
                                                <td>
                                                    <strong>使用时间</strong>
                                                </td>
                                                <td>
                                                    <strong>转入部门</strong>
                                                </td>
                                                <td>
                                                    <strong>转入人</strong>
                                                </td>
                                                <td>
                                                    <strong>转入时间</strong>
                                                </td>
                                                <td>
                                                    <strong>转入地点</strong>
                                                </td>
                                                <td>
                                                    <strong>原因</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <%#Container.ItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                                        ID="lblsyrid" Visible="false" runat="server" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label>
                                                    <asp:Label ID="syrbmid" runat="server" Text='<%#Eval("FBMID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblplace1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_bumen2" runat="server" name="ddl">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfbumen2" runat="server" />
                                                    <%--<asp:TextBox ID="txtzrbm" runat="server" ></asp:TextBox>--%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_user2" runat="server" name="ddl">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfSYR2" runat="server" />
                                                    <input type="hidden" runat="server" id="hidSyr2" name="hid" />
                                                    <input type="hidden" runat="server" id="hidSyrId2" name="hid" />
                                                    <%--<asp:TextBox ID="txtzrr" runat="server" ></asp:TextBox>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtplace2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtreason" runat="server" Text='<%#Eval("REASON")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批信息">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panSP">
                        <asp:HiddenField runat="server" ID="hidZDRID" />
                        <asp:HiddenField runat="server" ID="hidSPR1ID" />
                        <asp:HiddenField runat="server" ID="hidSPR2ID" />
                        <asp:HiddenField runat="server" ID="hidSPR3ID" />
                        <asp:Panel runat="server" ID="panZDR">
                            <table class="tab">
                                <tr style="background-color: #79CDCD">
                                    <td style="text-align: right">
                                        审批类型：
                                    </td>
                                    <td id="tdSPLX" colspan="3">
                                        <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                            Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                            <asp:ListItem Text="一级审批" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审批" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="三级审批" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px" width="20%">
                                        制单人：
                                    </td>
                                    <td width="30%">
                                        <asp:TextBox runat="server" ID="txtZDR" onfocus="this.blur()"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        制单时间：
                                    </td>
                                    <td width="30%">
                                        <asp:Label runat="server" ID="lbZDR_SJ" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        制单人建议：<asp:TextBox runat="server" ID="txtZDR_JY" Text="" TextMode="MultiLine" Width="90%"
                                            Font-Size="Medium"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panSPR1">
                            <table class="tab">
                                <tr style="background-color: #79CDCD">
                                    <td colspan="6" align="center" style="height: 30px">
                                        <asp:Label runat="server" ID="lb1" Text="第一级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" style="height: 30px;">
                                        审批人：
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:TextBox runat="server" ID="txtSPR1" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td width="15%">
                                        审批结论：
                                    </td>
                                    <td width="25%" align="center">
                                        <asp:RadioButtonList runat="server" ID="rblSPR1_JL" RepeatDirection="Horizontal"
                                            Style="margin: auto" Width="80%" RepeatColumns="2">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="15%">
                                        审批时间：
                                    </td>
                                    <td width="15%">
                                        <asp:Label runat="server" ID="lbSPR1_SJ" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<asp:TextBox runat="server" ID="txtSPR1_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panSPR2">
                            <table class="tab">
                                <tr style="background-color: #79CDCD">
                                    <td colspan="6" align="center" style="height: 30px;">
                                        <asp:Label runat="server" ID="lb3" Text="第二级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" style="height: 30px;">
                                        审批人：
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:TextBox runat="server" ID="txtSPR2" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                        <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td width="15%">
                                        审批结论：
                                    </td>
                                    <td width="25%" align="center">
                                        <asp:RadioButtonList runat="server" ID="rblSPR2_JL" RepeatDirection="Horizontal"
                                            Style="margin: auto" Width="80%" RepeatColumns="2">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="15%">
                                        审批时间：
                                    </td>
                                    <td width="15%">
                                        <asp:Label runat="server" ID="lbSPR2_SJ" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<asp:TextBox runat="server" ID="txtSPR2_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panSPR3">
                            <table class="tab">
                                <tr style="background-color: #79CDCD">
                                    <td colspan="6" align="center" style="height: 30px;">
                                        <asp:Label runat="server" ID="Label1" Text="第三级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" style="height: 30px;">
                                        审批人：
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:TextBox runat="server" ID="txtSPR3" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                        <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td width="15%">
                                        审批结论：
                                    </td>
                                    <td width="25%" align="center">
                                        <asp:RadioButtonList runat="server" ID="rblSPR3_JL" RepeatDirection="Horizontal"
                                            Style="margin: auto" Width="80%" RepeatColumns="2">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="15%">
                                        审批时间：
                                    </td>
                                    <td width="15%">
                                        <asp:Label runat="server" ID="lbSPR3_SJ" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<asp:TextBox runat="server" ID="txtSPR3_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
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
                        <input id="dep" name="dept" value="07">
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
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
