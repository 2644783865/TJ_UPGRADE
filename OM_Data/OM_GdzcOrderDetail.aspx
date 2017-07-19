<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcOrderDetail.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcOrderDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �̶��ʲ����&nbsp;&nbsp;&nbsp;&nbsp;
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

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
                    //                        $("#<%=txtSPR1.ClientID%>").val("��ΰ��");
                    //                    }
                    //                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR2.ClientID%>").val("��ΰ��");
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
                    //                        $("#<%=txtSPR1.ClientID%>").val("��ΰ��");
                    //                    }
                    //                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR2.ClientID%>").val("������");
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
        //**********������Ա�Ӵ���***********************
        var i;
        array = new Array();
        var $ddlDep;
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }

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

        //���ȷ��
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                //            console.log(r.st_name);
                $("#<%=txtshr.ClientID %>").val(r.st_name);
                $("#<%=shrid.ClientID %>").val(r.st_id);
                console.log($("#<%=txtshr.ClientID %>").val());
            }
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
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "") {
                        alert("��ѡ�������˺����ύ����!!!");
                        a = false;
                    }
                }
                else if ($(this).val() == "2") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2ID.ClientID %>").val() == "") {
                        alert("��ѡ�������˺����ύ����!!!");
                        a = false;
                    }
                }
                else if ($(this).val() == "3") {
                    if ($("#<%=txtSPR1.ClientID %>").val() == "" || $("#<%=hidSPR1ID.ClientID %>").val() == "" || $("#<%=txtSPR2.ClientID %>").val() == "" || $("#<%=hidSPR2ID.ClientID %>").val() == "" || $("#<%=txtSPR3.ClientID %>").val() == "" || $("#<%=hidSPR3ID.ClientID %>").val() == "") {
                        alert("��ѡ�������˺����ύ����!!!");
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
                                <asp:Button ID="btnSubmit" runat="server" Text="�ύ����" Visible="false" BackColor="LightGreen"
                                    Width="50px" Height="25px" OnClientClick="return btnSubmit_OnClientClick()" OnClick="btnSubmit_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="�� ��" BackColor="LightGreen" Width="40px"
                                    Height="25px" OnClientClick="return btnSubmit_OnClientClick();" OnClick="btnSave_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="�� ��" CausesValidation="false" OnClick="btnReturn_OnClick"
                                    BackColor="LightGreen" Width="40px" Height="25px" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:TabContainer runat="server" ID="TabContainer">
            <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="������Ϣ">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="panJBXX">
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center" colspan="5">
                                            <asp:Label ID="lbltitle1" runat="server" Text="�̶��ʲ���ⵥ" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5">
                                            ��ⵥ�ţ�<asp:Label ID="lblInCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            �Ƶ��ˣ�<asp:Label ID="lbl_creater" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right">
                                            ����Ա��
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtshr" runat="server" Width="150px" onfocus="this.blur()"></asp:TextBox>
                                            <input id="shrid" type="text" runat="server" readonly="readonly" style="display: none" />
                                            <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons1()">
                                                <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />
                                                ѡ��</asp:HyperLink>
                                            <span id="span1" runat="server" visible="false" class="Error">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="��ѡ������Ա��"
                                                ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            �������ڣ�<asp:Label ID="lblInDate" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="xinzeng" runat="server" Text=" ��������"></asp:Label><asp:TextBox ID="txtLines"
                                                runat="server" Width="75px"></asp:TextBox>
                                            <asp:Button ID="btnAdd" runat="server" Text="�� ��" OnClick="btnAdd_OnClick" CausesValidation="false" />
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnDelRow" runat="server" Text="ɾ����" OnClick="btnDelRow_OnClick"
                                                CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CellPadding="4" ForeColor="#333333" EmptyDataText="û��������ݣ�" OnRowDataBound="GridView1_RowDataBound">
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK" runat="server" CssClass="checkBoxCss" />
                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���뵥��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="lblCode" runat="server" Text='<%Eval("CODE") %>'></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddl_code" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfCODE" runat="server" Value='<%#Eval("CODE") %>' />
                                                    <asp:Label ID="lbl_CODE" runat="server" Visible="false" Text='<%#Eval("CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_bianhao" runat="server" Visible="false" Text='<%#Eval("BIANHAO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ʹ������(��)" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_nx" runat="server" Visible="false" Text='<%#Eval("NX") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��ֵ" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_jiazhi" runat="server" Visible="false" Text='<%#Eval("JIAZHI") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�̶��ʲ�����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("NAME") %>'></asp:TextBox>
                                                    <asp:Label ID="only_id" runat="server" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����1" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtType" runat="server" Text='<%#Eval("TYPE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="����2" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtType2" runat="server" Text='<%#Eval("TYPE2") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�ͺŻ����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Model" runat="server" Text='<%#Eval("MODEL") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��ע" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Note" runat="server" Text='<%#Eval("NOTE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ʹ�ò���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_bumen" runat="server" name="ddl">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfbumen" runat="server" Value='<%#Eval("SYBUMEN") %>' />
                                                    <asp:Label ID="lbl_BUMEN" runat="server" Visible="false" Text='<%#Eval("SYBUMEN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ʹ����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_user" runat="server" name="ddl">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfSYR" runat="server" Value='<%#Eval("SYR") %>' />
                                                    <asp:Label ID="lbl_user" runat="server" Visible="false" Text='<%#Eval("SYR") %>'></asp:Label>
                                                    <input type="hidden" runat="server" id="hidSyr" name="hid" />
                                                    <input type="hidden" runat="server" id="hidSyrId" name="hid" />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�ص�" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_place" runat="server" Text='<%#Eval("PLACE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="��������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNum" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="�������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumstore" runat="server" BorderStyle="None"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="�ջ�����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInNum" runat="server" onchange="amountrow(this)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="������Ϣ">
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
                                            �������ͣ�
                                        </td>
                                        <td id="tdSPLX" colspan="3">
                                            <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                                Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                                <asp:ListItem Text="һ������" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="��������" Value="2" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="��������" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px" width="20%">
                                            �Ƶ��ˣ�
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox runat="server" ID="txtZDR" onfocus="this.blur()"></asp:TextBox>
                                        </td>
                                        <td width="20%">
                                            �Ƶ�ʱ�䣺
                                        </td>
                                        <td width="30%">
                                            <asp:Label runat="server" ID="lbZDR_SJ" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            �Ƶ��˽��飺<asp:TextBox runat="server" ID="txtZDR_JY" Text="" TextMode="MultiLine" Width="90%"
                                                Font-Size="Medium"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="panSPR1">
                                <table class="tab">
                                    <tr style="background-color: #79CDCD">
                                        <td colspan="6" align="center" style="height: 30px">
                                            <asp:Label runat="server" ID="lb1" Text="��һ������" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" style="height: 30px;">
                                            �����ˣ�
                                        </td>
                                        <td width="15%" align="left">
                                            <asp:TextBox runat="server" ID="txtSPR1" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                            <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                                onclick="xr1()" align="middle" Style="cursor: pointer" title="ѡ��" />
                                        </td>
                                        <td width="15%">
                                            �������ۣ�
                                        </td>
                                        <td width="25%" align="center">
                                            <asp:RadioButtonList runat="server" ID="rblSPR1_JL" RepeatDirection="Horizontal"
                                                Style="margin: auto" Width="80%" RepeatColumns="2">
                                                <asp:ListItem Text="ͬ��" Value="y"></asp:ListItem>
                                                <asp:ListItem Text="��ͬ��" Value="n"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="15%">
                                            ����ʱ�䣺
                                        </td>
                                        <td width="15%">
                                            <asp:Label runat="server" ID="lbSPR1_SJ" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            �������飺<asp:TextBox runat="server" ID="txtSPR1_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                                Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="panSPR2">
                                <table class="tab">
                                    <tr style="background-color: #79CDCD">
                                        <td colspan="6" align="center" style="height: 30px;">
                                            <asp:Label runat="server" ID="lb3" Text="�ڶ�������" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" style="height: 30px;">
                                            �����ˣ�
                                        </td>
                                        <td width="15%" align="left">
                                            <asp:TextBox runat="server" ID="txtSPR2" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                            <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                                onclick="xr2()" align="middle" Style="cursor: pointer" title="ѡ��" />
                                        </td>
                                        <td width="15%">
                                            �������ۣ�
                                        </td>
                                        <td width="25%" align="center">
                                            <asp:RadioButtonList runat="server" ID="rblSPR2_JL" RepeatDirection="Horizontal"
                                                Style="margin: auto" Width="80%" RepeatColumns="2">
                                                <asp:ListItem Text="ͬ��" Value="y"></asp:ListItem>
                                                <asp:ListItem Text="��ͬ��" Value="n"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="15%">
                                            ����ʱ�䣺
                                        </td>
                                        <td width="15%">
                                            <asp:Label runat="server" ID="lbSPR2_SJ" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            �������飺<asp:TextBox runat="server" ID="txtSPR2_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                                Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="panSPR3">
                                <table class="tab">
                                    <tr style="background-color: #79CDCD">
                                        <td colspan="6" align="center" style="height: 30px;">
                                            <asp:Label runat="server" ID="Label1" Text="����������" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" style="height: 30px;">
                                            �����ˣ�
                                        </td>
                                        <td width="15%" align="left">
                                            <asp:TextBox runat="server" ID="txtSPR3" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                            <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                                onclick="xr3()" align="middle" Style="cursor: pointer" title="ѡ��" />
                                        </td>
                                        <td width="15%">
                                            �������ۣ�
                                        </td>
                                        <td width="25%" align="center">
                                            <asp:RadioButtonList runat="server" ID="rblSPR3_JL" RepeatDirection="Horizontal"
                                                Style="margin: auto" Width="80%" RepeatColumns="2">
                                                <asp:ListItem Text="ͬ��" Value="y"></asp:ListItem>
                                                <asp:ListItem Text="��ͬ��" Value="n"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="15%">
                                            ����ʱ�䣺
                                        </td>
                                        <td width="15%">
                                            <asp:Label runat="server" ID="lbSPR3_SJ" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            �������飺<asp:TextBox runat="server" ID="txtSPR3_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
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
                            <strong>ָ����Ա</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            �����Ų�ѯ��
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
                ����</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">ȡ��</a>
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
</asp:Content>
