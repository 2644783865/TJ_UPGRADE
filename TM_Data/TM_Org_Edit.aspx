<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_Org_Edit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Org_Edit" %>

<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="labprostru" runat="server" Text=""></asp:Label>
    <input id="hidAction" type="hidden" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../JS/ChoiceRcmd.js" type="text/javascript"></script>

    <script src="../JS/TMOrgInput.js" type="text/javascript"></script>

    <script type="text/javascript">

        /*原始数据 选择一类数据*/
        function selectOrgAll(obj) {
            var table = document.getElementById(getClientId().Id1);
            if (obj.checked) {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = true;
                }
            }
            else {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = false;
                }
            }
        }


        function ShowLastFive() {
            var date = new Date();
            var time = date.getTime();
            window.open("TM_OrgLastOne.aspx?action=<%=tsaid.Text %>&NoUse=" + time);

        }

        function ShowProTip() {
            window.showModalDialog("TM_Pro_Stru_List_Tip.aspx", '', "dialogHeight:400px;dialogWidth:950px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        }

        function ShowSetting() {
            var obj_showDiv = document.getElementById("clear_div");
            obj_showDiv.className = "show";
            ClearColumns(1);
        }

        function CloseDiv() {
            var obj_showDiv = document.getElementById("clear_div");
            obj_showDiv.className = "hidden";
        }
    </script>

    <script type="text/javascript">
        var Xishu_B_Shape;
        var Xishu_X_Shape;
        function getClientId() {
            var paraId1 = '<%= GridView1.ClientID %>'; //注册控件1
            var paraId2 = '<%= txtid.ClientID %>';
            var paraId3 = '<%= istid.ClientID %>';
            var paraId4 = '<%= lblNumber.ClientID %>';
            var paraId5 = '<%= txtBxishu.ClientID %>';
            var paraId6 = '<%= txtXxishu.ClientID %>';
            return { Id1: paraId1, Id2: paraId2, Id3: paraId3, Id4: paraId4, BXishu: paraId5, XXishu: paraId6 }; //生成访问器
        }

        /*助记码操作*/
        function autoCode(input) {
            var marid = document.getElementById(input.id).value;
            var table = document.getElementById(getClientId().Id1);
            var tr = table.getElementsByTagName("tr");
            var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var num = document.getElementById(getClientId().Id4).value;
            if (marid != null) {
                var marids = marid.split('|');
                if (marids.length < 8) { return; }
                //         ID + '|' + MNAME + '|' + GUIGE + '|' + CAIZHI + '|' + [TECHUNIT]+'|'+CAST([CONVERTRATE] AS VARCHAR)+'|'+[PURCUNIT]+'|'+[FUZHUUNIT]+'|'+cast(MWEIGHT as varchar)+'|'+GB AS CODE
                //tr[index].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = 
                //  tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value =
                tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value = marids[0];
                tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value = marids[1];
                tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = marids[2];
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value = marids[3];
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "1";
                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value = num * parseInt(tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value);
                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value = num * parseInt(tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value);
                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = "0"
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = marids[4];
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = "";
                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = "";
                //  tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "(" + marids[4] + ")-(" + marids[6] + "-" + marids[7] + ")";
                tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value = "";
                tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value = marids[8];
                tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value = marids[9];
                tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value = marids[6];
                var lilunzl = marids[8];
                if (marid.indexOf(" ") > -1) {
                    sub_marid = marid.substring(0, marid.indexOf(" "));
                }
                else {
                    sub_marid = marid;
                }
                var child_marid = sub_marid.substring(0, 5);
                var son_marid = child_marid.substring(0, 2);
                var cname = marids[1];
                //            tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value = sub_marid;
                if (child_marid == "01.01" || child_marid == "01.10" || child_marid == "01.05")//标准件
                {
                    tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "采";
                    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = num;
                    tr[index].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = marids[9];

                }
                else if (child_marid == "01.07" || child_marid == "01.14") {

                    if (marids[1].indexOf("钢板") > -1 || marids[1].indexOf("钢格板") > -1 || marids[1].indexOf("花纹板") > -1 || marids[1].indexOf("电焊网") > -1) {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "板";
                        tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "m2";
                        if (marids[1].indexOf("钢格板") > -1 || marids[1].indexOf("钢板网") > -1) {
                            tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "采";

                        }
                    }
                    else if (marids[1].indexOf("圆钢") > -1) {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "圆"; //毛坯
                        tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "m";
                    }
                    else if (marids[1].indexOf("重轨") > -1 || marids[1].indexOf("钢轨") > -1 || marids[1].indexOf("轻轨") > -1 || marids[1].indexOf("型钢") > -1 || marids[1].indexOf("扁钢") > -1 || marids[1].indexOf("焊管") > -1 || marids[1].indexOf("焊接管") > -1 || marids[1].indexOf("无缝管") > -1 || marids[1].indexOf("无缝钢管") > -1 || marids[1].indexOf("槽钢") > -1 || marids[1].indexOf("角钢") > -1 || marids[1].indexOf("工字钢") > -1 || marids[1].indexOf("方钢") > -1 || marids[1].indexOf("矩形管") > -1 || marids[1].indexOf("轨道") > -1 || marids[1].indexOf("管") > -1 || marids[1].indexOf("铜棒") > -1) {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "型"; //毛坯

                        tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "m";
                    }
                }
                else {
                    if (child_marid == "01.09") {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "锻";
                        tr[index].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = marids[9];
                        tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = num;
                    }
                    else if (child_marid == "01.08") {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "铸";
                        tr[index].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = marids[9];
                        tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = num;
                    }
                    else if (child_marid == "01.11") {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "采购成品";
                        tr[index].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value = marids[9];
                        tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = num;
                    }
                    else if (child_marid == "01.06") {
                        tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value = "非";
                    }
                    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = lilunzl;
                    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = lilunzl;
                }


            }
        }

        function DoubleNum() {
            var table = document.getElementById(getClientId().Id1);
            var tr = table.getElementsByTagName("tr");
            for (i = 1; i < table.rows.length; i++) {
                objstr = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (objstr.checked) {
                    if (document.getElementById('txtXiShu') != null) {
                        var number = document.getElementById('txtXiShu').value; //系数
                        var dantaishuliang = tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //单台数量

                        if (dantaishuliang != "") {
                            var shuliang = parseInt(dantaishuliang) * parseInt(number);

                            var cailiaodzh = tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                            var tudzh = tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //总重
                            var bgzmy = tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                            var p_mianyu = tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");
                            var cailiaozongchang = tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                            var cailiaochangdu = tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                            var cailiaoType = tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                            var fix = tr[i].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
                            GetXiShu_TMOrg(fix);

                            tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value = shuliang;
                            tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value = shuliang;
                            tr[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = parseFloat(cailiaodzh) * parseFloat(shuliang);
                            tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = parseFloat(tudzh) * parseFloat(shuliang);


                            if (cailiaoType == "板" && fix == "N") {
                                tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = parseFloat(bgzmy) * parseFloat(shuliang) * parseFloat(Xishu_B_Shape);
                                tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = parseFloat(bgzmy) * parseFloat(shuliang) * parseFloat(Xishu_B_Shape);

                            }
                            else if ((cailiaoType == "圆" || cailiaoType == "型") && fix == "N") {
                                tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = parseFloat(cailiaochangdu) * parseFloat(shuliang) * parseFloat(Xishu_X_Shape);
                                tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(cailiaochangdu) * parseFloat(shuliang) * parseFloat(Xishu_X_Shape) / 1000).toFixed(4);
                            }
                            else {

                                tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = shuliang;
                            }

                        }
                    }
                    else {
                        alert('请输入倍乘系数');
                    }

                }

            }
        }
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 18%">
                                    任务号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                    <input id="eng_type" type="text" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 18%">
                                    合同号:<asp:Label ID="lab_contract" runat="server"></asp:Label>
                                    <input id="con_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 18%">
                                    项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                    <input id="pro_name" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 25%">
                                    设备名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    总序：<asp:Label ID="lblZongxu" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div>
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过15条！！！" Width="40"
                                Text="插入" OnClientClick="return insert()" OnClick="btninsert_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Width="40" Text="删除" OnClientClick="return check()"
                                OnClick="btndelete_Click" />
                        </td>
                        <td>
                            全选/取消<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
                            <asp:Button ID="btnSelect_org" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_org_OnClick"
                                Text="连选" />
                        </td>
                        <td align="right" style="width: 10%;">
                            <strong>计划系数:</strong>
                        </td>
                        <td colspan="3">
                            (板)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'
                                style="width: 30px" value="1.05" />(型/圆钢)<input id="txtXxishu" runat="server" style="width: 30px"
                                    onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'
                                    type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width: 30px"
                                        disabled="disabled" type="text" value="1" />
                        </td>
                        <td>
                            数量:<asp:TextBox ID="lblNumber" runat="server" Width="20px" onblur='var pattem=/^\d{1,3}?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";this.foucs();}'></asp:TextBox>
                            <asp:Button ID="btnsave" runat="server" Text="保 存" Width="50" OnClientClick="return BomInputCheck();"
                                OnClick="btnsave_Click" />
                            <asp:Button ID="btnChange" runat="server" Text="执行变更" Width="60" OnClientClick="return confirm('确定执行变更吗？若确定，将更新该总序下的所有记录的变更状态');return BomInputCheck();"
                                OnClick="btnsave_Click" CommandName="ChangeAll" />
                            <asp:Button ID="btnChange2" runat="server" Text="变更明细" Width="60" OnClientClick="return confirm('确定执行变更吗？若确定，将更新该总序下的所有记录的变更状态');return BomInputCheck();"
                                CommandName="ChangeMS" OnClick="btnsave_Click" />
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="center">
                            <strong>倍乘数量:</strong><input id="txtXiShu" style="width: 20px;" value="1" type="text"
                                onblur='var pattem=/^\d{1,3}?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";this.foucs();}' />
                            <input id="btnXiShu" type="button" onclick="DoubleNum()" value="确定" title="点击确定之后，所有勾选的记录的单台数量将会相乘前面的数字" />
                        </td>
                        <td>
                            <asp:HyperLink ID="hylClear" runat="server" CssClass="hand">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />清空选定列</asp:HyperLink>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender3" TargetControlID="hylClear" OffsetX="-320"
                                OffsetY="20" PopupControlID="Panel3" runat="server">
                            </cc1:HoverMenuExtender>
                            <asp:Panel ID="Panel3" runat="server" Style="display: none;">
                                <table width="320px" style='background-color: #f3f3f3; border: #B9D3EE 3px solid;
                                    font-size: 10pt; font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                    <tr>
                                        <td>
                                            以下勾选列将清空
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="ckbClearColumns" RepeatColumns="3" RepeatDirection="Horizontal"
                                                runat="server">
                                                <asp:ListItem Text="图号" Value="2-tuhao"></asp:ListItem>
                                                <asp:ListItem Text="材料长度/总长" Value="8-cailiaocd,19-cailiaozongchang"></asp:ListItem>
                                                <asp:ListItem Text="数量" Value="11-shuliang"></asp:ListItem>
                                                <asp:ListItem Text="总序" Value="4-zongxu"></asp:ListItem>
                                                <asp:ListItem Text="材料宽度" Value="9-cailiaokd"></asp:ListItem>
                                                <asp:ListItem Text="图纸单重/图纸总重" Value="12-dzh,13-zongzhong"></asp:ListItem>
                                                <asp:ListItem Text="面域" Value="18-bgzmy"></asp:ListItem>
                                                <asp:ListItem Text="材料单重/总重" Value="14-cailiaodzh,15-cailiaozongzhong"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnClearColumn" runat="server" OnClick="btnClearColumn_OnClick" OnClientClick="return confirm('确认清空以上选定列值吗？');"
                                                Text="清 空" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal"
                RepeatColumns="18" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"
                AutoPostBack="true" RepeatLayout="Table">
            </asp:CheckBoxList>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="2" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="lblIndex" onmouseover="Show_div_othersetting(this);" onkeyup="grControlFocus(this)"
                                readonly="readonly" style="border-style: none; width: 20px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                            <div style="padding-bottom: 1px;">
                            </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" style="border-style: none; width: 100px" type="text"
                                onfocus="this.select();" onblur="OrgAutoTuHao(this);" value='<%#Eval("BM_TUHAO") %>'
                                ondblclick="ShowInput(this);" onkeydown="grControlFocus(this)" /><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="zongxu" runat="server" onblur="OrgAutoXuhao(this)" onmouseover="this.title=this.value;"
                                ondblclick="ShowInput(this);" style="border-style: none; width: 60px" type="text"
                                value='<%#Eval("BM_ZONGXU") %>' onkeydown="grControlFocus(this)" /><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码">
                        <ItemTemplate>
                            <asp:TextBox ID="marid" runat="server" BorderStyle="None" onfocus="this.select();"
                                onchange="autoCode(this)" Text='<%# DataBinder.Eval(Container.DataItem, "BM_MARID")%>'
                                Width="80px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True" CompletionInterval="10">
                            </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" style="border-style: none; width: 80px" ondblclick="ShowInput(this);"
                                title="请在输入物料编码后再修改此项！！！" onfocus="this.select();" type="text" value='<%#Eval("BM_CHANAME") %>'
                                onkeydown="grControlFocus(this)" /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料规格" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoguige" runat="server" readonly="readonly" style="border-style: none;
                                width: 55px" type="text" value='<%#Eval("BM_MAGUIGE") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="caizhi" runat="server" readonly="readonly" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAQUALITY") %>' onfocus="this.select();"
                                onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度">
                        <ItemTemplate>
                            <input id="cailiaocd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 40px" type="text" value='<%#Eval("BM_MALENGTH") %>' onfocus="this.select();"
                                onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度">
                        <ItemTemplate>
                            <input id="cailiaokd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 40px" type="text" value='<%#Eval("BM_MAWIDTH") %>' onfocus="this.select();"
                                onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下料备注">
                        <ItemTemplate>
                            <input id="note" runat="server" style="border-style: none; width: 80px" type="text"
                                ondblclick="ShowInput(this);" value='<%#Eval("BM_NOTE") %>' onkeydown="grControlFocus(this)"
                                onfocus="this.select();" /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="shuliang" runat="server" onchange="autoshuliang(this)" class="notbrk"
                                style="border-style: none; width: 36px" type="text" value='<%#Eval("BM_SINGNUMBER") %>'
                                onfocus="this.select();" onkeydown="grControlFocus(this)" />
                            |&nbsp;<input id="total_shuliang" runat="server" tabindex="-1" class="notbrk" title="单台数量*台数,不能修改"
                                onfocus="this.blur();" readonly="readonly" value='<%#Eval("BM_NUMBER") %>' style="border-style: none;
                                width: 36px" type="text" />
                            |&nbsp;<input id="plan_shuliang" runat="server" tabindex="-1" onchange="autop_shuliang(this)"
                                class="notbrk" title="材料计划数量" onfocus="this.select();" style="border-style: none;
                                width: 36px" value='<%#Eval("BM_PNUMBER") %>' type="text" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸单重(kg)">
                        <ItemTemplate>
                            <input id="tudz" runat="server" title="kg" style="border-style: none; width: 50px"
                                type="text" onchange="TudanZhongBom(this);" value='<%#Eval("BM_TUUNITWGHT") %>'
                                onfocus="this.select();" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸总重(kg)">
                        <ItemTemplate>
                            <input id="tuzhiZZ" runat="server" title="kg" style="border-style: none; width: 50px"
                                type="text" value='<%#Eval("BM_TUTOTALWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重(kg)">
                        <ItemTemplate>
                            <input id="cailiaodzh" runat="server" title="kg" style="border-style: none; width: 50px"
                                onchange="auto2(this);" type="text" value='<%#Eval("BM_MAUNITWGHT") %>' onfocus="this.select();"
                                onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总重(kg)">
                        <ItemTemplate>
                            <input id="cailiaozongzhong" title="kg:材料计划" runat="server" onchange="MarTotalWeightChange(this);"
                                style="border-style: none; width: 50px" type="text" value='<%#Eval("BM_MATOTALWGHT") %>'
                                onfocus="this.select();" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="技术单位" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="labunit" runat="server" readonly="readonly" style="border-style: none;
                                width: 30px" type="text" value='<%#Eval("BM_TECHUNIT") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料用量" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="txtYongliang" runat="server" style="border-style: none; width: 40px" type="text"
                                value='<%#Eval("BM_YONGLIANG") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="bgzmy" runat="server" onchange="automy(this)" title="平方米:输入单个数量面域即可，不包含双面！！！"
                                style="border-style: none; width: 45px" type="text" value='<%#Eval("BM_MABGZMY") %>'
                                onfocus="this.select();" onkeydown="grControlFocus(this)" />
                            |&nbsp;<input id="mpmy" runat="server" title="平方米:按【平米】采购计划，在此输入单个面域！！！" style="border-style: none;
                                width: 45px" type="text" value='<%#Eval("BM_MPMY") %>' onfocus="this.select();"
                                onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="";this.select();}}'
                                onkeydown='grControlFocus(this.parentNode.getElementsByTagName("input")[0])' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="cailiaozongchang" runat="server" style="border-style: none; width: 50px"
                                type="text" value='<%#Eval("BM_MATOTALLGTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料种类" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="cailiaoType" runat="server" onfocus="findChoice(this,1,20);this.select();"
                                style="border-style: none; width: 32px" type="text" onchange="ChangeofMarShape(this);"
                                value='<%#Eval("BM_MASHAPE") %>' onkeydown="grControlFocus(this)" />
                            <div class="hidden" style="position: absolute; background-color: #f3f3f3; cursor: hand;
                                border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                <ul style="list-style-type: square; text-align: left; line-height: normal;">
                                </ul>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下料方式" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="xialiao" runat="server" onfocus="findChoice(this,2,21);this.select();"
                                style="border-style: none; width: 32px" type="text" value='<%#Eval("BM_XIALIAO") %>'
                                onkeydown="grControlFocus(this)" />
                            <div class="hidden" style="position: absolute; background-color: #f3f3f3; cursor: hand;
                                border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                <ul style="list-style-type: square; text-align: left; line-height: normal;">
                                </ul>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工艺流程">
                        <ItemTemplate>
                            <input id="process" runat="server" onfocus="findChoice(this,3,22);this.select();"
                                style="border-style: none; width: 100px" type="text" value='<%#Eval("BM_PROCESS") %>'
                                onkeydown="grControlFocus(this)" />
                            <div class="hidden" style="position: absolute; background-color: #f3f3f3; cursor: hand;
                                border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                <ul style="list-style-type: square; text-align: left; line-height: normal;">
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="ku" runat="server" style="border-style: none; width: 30px" type="text"
                                value='<%#Eval("BM_KU") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="理论重量(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="lilunzhl" runat="server" readonly="readonly" style="border-style: none;
                                width: 35px" type="text" value='<%#Eval("BM_THRYWGHT") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="biaozhun" runat="server" readonly="readonly" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_STANDARD") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="采购单位" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="caigoudanwei" runat="server" readonly="readonly" style="border-style: none;
                                width: 40px" type="text" value='<%#Eval("BM_CAIGOUUNIT") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="制作明细">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKeyComponents" runat="server" onkeydown="grControlFocus(this)"
                                SelectedValue='<%#Eval("BM_ISMANU") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="定尺">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFixedSize" runat="server" onkeydown="grControlFocus(this)"
                                onchange="CtrlFixSize(this);" SelectedValue='<%#Eval("BM_FIXEDSIZE") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料计划">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWmp" runat="server" onkeydown="grControlFocus(this)" SelectedValue='<%#Eval("BM_WMARPLAN") %>'>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="zongbeizhu" runat="server" onfocus="this.select();" onkeydown="grControlFocus(this)"
                                style="border-style: none; width: 60px;" type="text" value='<%#Eval("BM_ALLBEIZHU") %>' /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="450px" TableWidth="100%" />
            </yyc:SmartGridView>
            <div>
                <input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
