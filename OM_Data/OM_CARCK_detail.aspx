<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_CARCK_detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CARCK_detail"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车品出库&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PrimaryContent" runat="server">
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </cc1:ToolkitScriptManager>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#<%=panSPR1.ClientID%>").hide();
            $("#<%=panSPR2.ClientID%>").hide();

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

            });
        })

        function rblSPJB_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").hide();

                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();

                    //                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR1.ClientID%>").val("蔡伟疆");
                    //                    }
                    //                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR2.ClientID%>").val("周文轶");
                    //                    }
                }

            });
        }
     function check_num(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,6})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
        }

    </script>

    <script language="javascript" type="text/javascript">
        //        function amountrow(obj) {
        //            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
        //            var tr = table.getElementsByTagName("tr");
        //            var numstore = parseFloat(obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value);
        //            var innum = parseFloat(obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value);
        //            if (innum != null) {
        //                obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = (numstore + innum).toFixed(2);
        //            }          
        //        }
        //**********弹出人员子窗口***********************
        var i;
        array = new Array();
        var $ddlDep;

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
                $("#<%=hidSPR1_ID.ClientID %>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=txtSPR2.ClientID %>").val(r.st_name);
                $("#<%=hidSPR2_ID.ClientID %>").val(r.st_id);
            }

            $('#win').dialog('close');
        }

        $(function() {
            //console.log($("select[name*=ddl_bumen]"));
            $("select[name*=ddl_bumen]").change(function() {
                $ddlDep = $(this);
                var depId = $(this).val();
                $.ajax({
                    url: 'OM_AjaxHandler.aspx',
                    type: 'post',
                    data: { method: 'FindDepPeo', Id: depId },
                    dataType: 'json',
                    success: function(data) {
                        //console.log($ddlDep.parent().parent().find("select[name*=ddl_user]"));
                        var $ddlUser = $ddlDep.parent().parent().find("select[name*=ddl_user]");
                        $ddlUser.empty();
                        for (i = 0; i < data.length; i++) {
                            var option = $("<option ></option>").val(data[i].st_id).html(data[i].st_name);
                            $ddlUser.append(option);

                        }
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyr]").val($ddlUser.find(":selected").text());
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyrId]").val($ddlUser.val());

                    }
                });
            });
        });

        $(function() {
            $("select[name*=ddl_user]").change(function() {
                $(this).parent().find("input[type=hidden][name*=hidSyr]").val($(this).find(":selected").text());
                $(this).parent().find("input[type=hidden][name*=hidSyrId]").val($(this).val());
                // console.log($("input[type=hidden][name*=hidSyr]").val());
                //console.log($("input[type=hidden][name*=hidSyrId]").val());
            });
        });

        function btnSubmit_OnClientClick() {
            var a = true;
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1_ID.ClientID %>").val() == "") {
                        alert("请选择审批人后再提交审批!!!");
                        a = false;
                    }
                }
                else if ($(this).val() == "2") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1_ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2_ID.ClientID %>").val() == "") {
                        alert("请选择审批人后再提交审批!!!");
                        a = false;
                    }
                }

            });
            return a;
        }
             
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="提 交" BackColor="LightGreen" Width="40px"
                                    Height="25px" OnClientClick="return btnSubmit_OnClientClick();" OnClick="btnSave_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                    BackColor="LightGreen" Width="40px" Height="25px" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:TabContainer runat="server" ID="TabContainer">
            <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="基本信息">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="panJBXX">
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center" colspan="5">
                                            <asp:Label ID="lbltitle1" runat="server" Text="车辆出库单" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            制单人：<asp:Label ID="lbl_creater" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="left">
                                            制单日期：<asp:Label ID="lblInDate" runat="server"></asp:Label>
                                        </td>
                                </table>
                                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle">
                                                <td>
                                                    <strong>序号</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                    <strong>规格</strong>
                                                </td>
                                                <td>
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>单价</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <%#Container.ItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbh" runat="server" Text='<%#Eval("SP_MC")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("SP_GG")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_shuliang" runat="server" Text='<%#Eval("SP_SL")%>' onblur="javascript:check_num(this)"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbdanwei" runat="server" Text='<%#Eval("SP_DANWEI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldanjia" runat="server" Text='<%#Eval("SP_DJ")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="审批信息">
                <ContentTemplate>
                    <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                        <asp:Panel runat="server" ID="panSP">
                            <asp:HiddenField runat="server" ID="hidZDRID" />
                            <asp:HiddenField runat="server" ID="hidSPR1_ID" />
                            <asp:HiddenField runat="server" ID="hidSPR2_ID" />
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
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            审批建议：<asp:TextBox runat="server" ID="txtSPR2_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
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
    </div>
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
                            <input id="dep" name="dept" value="02" />
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
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
</asp:Content>
