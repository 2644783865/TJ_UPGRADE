<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeAddPiLiang.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeAddPiLiang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    批量增加人员考核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
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
        function SelTechPersons5() {
            $("#hidPerson").val("fifth");
            SelPersons();
        }
        //**********审核选人***********************
        function SelPersonssh1() {
            $("#hidPerson").val("firstsh");
            SelPersons();
        }

        function SelPersonssh2() {
            $("#hidPerson").val("secondsh");
            SelPersons();
        }
        function SelPersonssh3() {
            $("#hidPerson").val("thirdsh");
            SelPersons();
        }
        function SelPersonssh4() {
            $("#hidPerson").val("fourthsh");
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
            else if (id == "fifth") {
                $("#<%=txt_fifth.ClientID %>").val(r.st_name);
                $("#<%=fifthId.ClientID %>").val(r.st_id);
            }
            
            else if (id == "firstsh") {
                $("#<%=txtshname1.ClientID %>").val(r.st_name);
                $("#<%=txtshid1.ClientID %>").val(r.st_id);
            }
            else if (id == "secondsh") {
                $("#<%=txtshname2.ClientID %>").val(r.st_name);
                $("#<%=txtshid2.ClientID %>").val(r.st_id);
            }
            else if (id == "thirdsh") {
                $("#<%=txtshname3.ClientID %>").val(r.st_name);
                $("#<%=txtshid3.ClientID %>").val(r.st_id);
            }
            else if (id == "fourthsh") {
                $("#<%=txtshname4.ClientID %>").val(r.st_name);
                $("#<%=txtshid4.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }




        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
            else {
                if (obj.value > 100) {
                    alert("超过最大分数！！！");
                    obj.value = "0";
                }
            }
        }

        function Calculate() {
            var fenbl = $("#<%=lb_Result.ClientID %>").html();
            var zongHe = 0;
            var bl = $("#<%=lblBL.ClientID %>").html();
            var arr = bl.split('|');
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] != 0) {
                    var fenScore = 0;
                    $("input[name*=kh_Score" + i + "]").each(function(j, item) {
                        var x = j + 1;
                        //   console.log($("#gr tr:eq(" + x + ")").find("td[name*=weight]").html().replace('%',''));
                        var bl1 = $("#gr tr:eq(" + x + ")").find("td[name*=weight]").html().replace('%', '')
                        fenScore += $(this).val() * bl1;
                    });
                    var y = i + 1;
                    $("input[name*=txtResult" + y + "]").val(fenScore / 100);
                    zongHe += (fenScore / fenbl) * arr[i];
                }
            }
            //  alert(zongHe / 100);
            $("#<%=txt_Result.ClientID %>").val(zongHe / 100);
            //自评
            var fenOwn = 0;
            $("input[name*=kh_ScoreOwn]").each(function(j, item) {
                var x = j + 1;
                //   console.log($("#gr tr:eq(" + x + ")").find("td[name*=weight]").html().replace('%',''));
                var bl1 = $("#gr tr:eq(" + x + ")").find("td[name*=weight]").html().replace('%', '')
                fenOwn += $(this).val() * bl1;
            });
            $("#<%=txtResult0.ClientID %>").val(fenOwn / 100);


        }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnAudit" runat="server" Text="开始评价" OnClick="btnAudit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClientClick="return Test();"
            OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_KaoHeList.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="hidden" id="hidId" runat="server" />
        <input type="hidden" id="hidConext" runat="server" />
        <input type="hidden" id="hidState" runat="server" />
        <input type="hidden" id="hidAction" runat="server" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="考核信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center;">
                        <div style="text-align: center;">
                            <h2>
                                <asp:Label ID="lbtitle" runat="server"></asp:Label></h2>
                        </div>
                        <br />
                        <asp:Panel ID="Panel0" runat="server">
                            <table width="1150px">
                                <tr>
                                    <td>
                                        制单人姓名：
                                    </td>
                                    <td>
                                        <asp:Label ID="lb1" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        考核时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                        <td>
                                        选择考核年月：
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Width="100px" ID="txtKhNianYue" class="easyui-datebox"
                                            data-options="formatter:function(date){var y = date.getFullYear();var m = date.getMonth()+1;return y+'-'+m;},editable:false"></asp:TextBox>
                                    </td>
                                    <td>
                                        考核类型：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlType" runat="server" Width="100px" AutoPostBack="true">
                                            <asp:ListItem Text="人员月度考核" Value="人员月度考核"></asp:ListItem>
                                            <asp:ListItem Text="人员年度考核" Value="人员年度考核"></asp:ListItem>
                                            <asp:ListItem Text="入职转正考核" Value="入职转正考核"></asp:ListItem>
                                            <asp:ListItem Text="合同续订考核" Value="合同续订考核"></asp:ListItem>
                                            <asp:ListItem Text="部门月度考核" Value="部门月度考核"></asp:ListItem>
                                            <asp:ListItem Text="部门年度考核" Value="部门年度考核"></asp:ListItem>
                                            <asp:ListItem Text="合同主体转移考核" Value="合同主体转移考核"></asp:ListItem>
                                            <asp:ListItem Text="实习生实习期考核" Value="实习生实习期考核"></asp:ListItem>
                                            <asp:ListItem Text="员工岗位调整考核" Value="员工岗位调整考核"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                   
                                    <td>
                                        考核比重：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBL" runat="server" ForeColor="Red" Width="100px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        被考核人部门：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_Depart_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        被考核人职位：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Position" runat="server" Width="120px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_Position_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                   <td>
                                        是否反馈：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFankui" runat="server" Width="100px">
                                            <asp:ListItem Value="0" Text="是"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="否"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                       是否自评： 
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_zipstate" runat="server" Width="100px">
                                        <asp:ListItem Text="是" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        模板所在部门：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPartMB" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_Person_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        考核模板：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlKaohMB" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                
                                    <td>
                                        <asp:Button ID="btn" runat="server" Text="加载考核模板" OnClick="btn_Click" />
                                    </td>
                                </tr>
                                 <tr>
                                  <td>
                                        被考核人姓名：
                                    </td>
                                    <td colspan="9">
                                        <asp:CheckBoxList  runat="server" ID="cblPerson" RepeatColumns="12"></asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        评分标准：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox runat="server" ID="txtPFBZ" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                               
                            </table>
                        </asp:Panel>
                        <br />
                        <div>
                            <hr />
                        </div>
                        <br />
                        <div style="text-align: center; overflow: auto; height: 400px">
                            <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="white-space: normal">
                                <asp:Repeater ID="Det_Repeater" runat="server">
                                    <HeaderTemplate>
                                        <tr class="tableTitle headcolor">
                                            <td>
                                                <strong>序&nbsp;号</strong>
                                            </td>
                                            <td runat="server" id="col1">
                                                <div style="width: 100px">
                                                    <asp:Label ID="kh_Col1" runat="server" Font-Bold="true"></asp:Label></div>
                                            </td>
                                            <td runat="server" id="col2">
                                                <div style="width: 320px">
                                                    <asp:Label ID="kh_Col2" runat="server" Font-Bold="true" Style="width: 200px"></asp:Label></div>
                                            </td>
                                            <td runat="server" id="col3">
                                                <div style="width: 320px">
                                                    <asp:Label ID="kh_Col3" runat="server" Width="150px" Font-Bold="true"></asp:Label></div>
                                            </td>
                                            <td runat="server" id="col4">
                                                <asp:Label ID="kh_Col4" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col5">
                                                <asp:Label ID="kh_Col5" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col6">
                                                <asp:Label ID="kh_Col6" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col7">
                                                <asp:Label ID="kh_Col7" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col8">
                                                <asp:Label ID="kh_Col8" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col9">
                                                <asp:Label ID="kh_Col9" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col10">
                                                <asp:Label ID="kh_Col10" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col11">
                                                <asp:Label ID="kh_Col11" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td runat="server" id="col12">
                                                <asp:Label ID="kh_Col12" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <strong>完成情况</strong>
                                            </td>
                                            <td>
                                                <strong>自评分</strong>
                                            </td>
                                            <td>
                                                <strong>一级考核</strong>
                                            </td>
                                            <td>
                                                <strong>二级考核</strong>
                                            </td>
                                            <td>
                                                <strong>三级考核</strong>
                                            </td>
                                            <td>
                                                <strong>四级考核</strong>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" Text="权重" runat="server" Font-Bold="true" Width="150px"></asp:Label>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                            style="vertical-align: middle; text-align: justify">
                                            <td>
                                                <div style="width: 50px">
                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                </div>
                                            </td>
                                            <td id="td1" runat="server">
                                                <%#Eval("kh_Cont1").ToString().Replace("\n","<br />") %>
                                            </td>
                                            <td id="td2" runat="server">
                                                <%#Eval("kh_Cont2").ToString().Replace("\n","<br />") %>
                                            </td>
                                            <td id="td3" runat="server">
                                                <%#Eval("kh_Cont3").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td4" runat="server">
                                                <%#Eval("kh_Cont4").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td5" runat="server">
                                                <%#Eval("kh_Cont5").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td6" runat="server">
                                                <%#Eval("kh_Cont6").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td7" runat="server">
                                                <%#Eval("kh_Cont7").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td8" runat="server">
                                                <%#Eval("kh_Cont8").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td9" runat="server">
                                                <%#Eval("kh_Cont9").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td10" runat="server">
                                                <%#Eval("kh_Cont10").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td11" runat="server">
                                                <%#Eval("kh_Cont11").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td id="td12" runat="server">
                                                <%#Eval("kh_Cont12").ToString().Replace("\n", "<br />")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_Note" runat="server" Width="250px" TextMode="MultiLine" Text='<%#Eval("kh_Note") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_ScoreOwn" name="score0" runat="server" Width="100px" CssClass="center"
                                                    Text='<%#Eval("kh_ScoreOwn") %>' onkeyup="Calculate()"  onblur="Calculate()"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_Score0" name="score0" runat="server" Width="100px" CssClass="center"
                                                    onkeyup="Calculate()"  onblur="Calculate()" Text='<%#Eval("kh_Score1") %>'></asp:TextBox>
                                                <input id="hidId" type="hidden" runat="server" value='<%#Eval("Id") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_Score1" name="score1" runat="server" Width="100px" CssClass="center"
                                                    onkeyup="Calculate()"  onblur="Calculate()" Text='<%#Eval("kh_Score2") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_Score2" name="score2" runat="server" Width="100px" CssClass="center"
                                                    onkeyup="Calculate()"  onblur="Calculate()" Text='<%#Eval("kh_Score3") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kh_Score3" name="score3" runat="server" Width="100px" CssClass="center"
                                                    onkeyup="Calculate()"  onblur="Calculate()" Text='<%#Eval("kh_Score4") %>'></asp:TextBox>
                                            </td>
                                            <td id="weight" runat="server" name="weight">
                                                <%#Eval("kh_Weight") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr runat="server" id="tr_foot" visible="false">
                                    <td runat="server" id="foot">
                                        <strong>评分结果</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_Result" runat="server"></asp:Label>%
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResult0" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResult1" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResult2" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResult3" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResult4" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Result" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Visible="false">
                                没有记录!</asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="考核信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-outer">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Panel runat="server" ID="Panel1">
                            <tr>
                                <td align="center">
                                    一级考核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                考核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                            </td>
                                            <td align="center" style="width: 20%">
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
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel2">
                            <tr>
                                <td align="center">
                                    二级考核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                考核人
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
                                            </td>
                                            <td align="center" style="width: 20%">
                                            </td>
                                            <td align="center" style="width: 10%">
                                                考核时间
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
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel3">
                            <tr>
                                <td align="center">
                                    三级考核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                考核人
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
                                            </td>
                                            <td align="center" style="width: 20%">
                                            </td>
                                            <td align="center" style="width: 10%">
                                                考核时间
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
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel5">
                            <tr>
                                <td align="center">
                                    四级考核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                考核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_fifth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="fifthId" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect5" runat="server" CssClass="hand" onClick="SelTechPersons5()">
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                            </td>
                                            <td align="center" style="width: 20%">
                                            </td>
                                            <td align="center" style="width: 10%">
                                                考核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="fif_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="fif_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel4">
                            <tr>
                                <td align="center">
                                    被考核人反馈
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                被考核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_fourth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="fourthId" type="text" runat="server" readonly="readonly" style="display: none" />
                                            </td>
                                            <td align="center" style="width: 10%">
                                            </td>
                                            <td align="center" style="width: 20%">
                                            </td>
                                            <td align="center" style="width: 10%">
                                                考核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="fourth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="fourth_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        
        
        
        <asp:TabPanel runat="server" ID="TabPanel3" TabIndex="2" Width="100%" HeaderText="审核信息">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblSHJS_OnSelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Text="不需审核" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="四级审核" Value="4"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    <asp:Label ID="lbtitlesh" runat="server"></asp:Label></h2>
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr id="yjshh" runat="server">
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
                                                <asp:TextBox ID="txtshname1" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="txtshid1" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="Hylsh1" runat="server" CssClass="hand" onClick="SelPersonssh1()" Visible="false">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsh1" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lbshtime1" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinionsh1" runat="server" Height="42px" TextMode="MultiLine"
                                                    Width="100%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="ejshh" runat="server">
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
                                                <asp:TextBox ID="txtshname2" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="txtshid2" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="Hylsh2" runat="server" CssClass="hand" onClick="SelPersonssh2()" Visible="false">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsh2" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lbshtime2" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinionsh2" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="sjshh" runat="server">
                                <td align="center">
                                    三级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtshname3" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="txtshid3" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="Hylsh3" runat="server" CssClass="hand" onClick="SelPersonssh3()" Visible="false">
                                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsh3" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lbshtime3" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinionsh3" runat="server" TextMode="MultiLine" Width="100%"
                                                Height="42px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="foursh" runat="server">
                                <td align="center">
                                    四级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtshname4" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="txtshid4" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="Hylsh4" runat="server" CssClass="hand" onClick="SelPersonssh4()" Visible="false">
                                                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsh4" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lbshtime4" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinionsh4" runat="server" TextMode="MultiLine" Width="100%"
                                                Height="42px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
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
