<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHe_JXGZ_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHe_JXGZ_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    部门月度绩效审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="SM_JS/NameQuery.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            CaculateZong();
        });

$(function(){
$("#<%=txtPer.ClientID %>").change(function(){
var arr= $(this).val().split('|');
$(this).val(arr[1]);
$("#<%=hidPerId.ClientID %>").val(arr[0]);
});
});

        function CaculateZong() {
            var Zonghe = 0;
            var ZongXiShu = 0;
            var ZongZh = 0;
            $("#gr span[name*=lblPosition]").each(function() {
                var posName = $(this).html();
                var department=$(this).parent().parent().find("span[name*=lblDEPNAME]").html();
                var money = $(this).parent().parent().find("input[name*=txtMoney]").val();
                if (posName!="部长" && posName.indexOf("总监") == -1&&posName!="总经理助理兼部长") {
                    if((department=="质量部")&&posName=="部长助理")
                    {
                          $(this).parent().parent().find("input[name*=txtMoney]").attr("readonly", "readonly");                  
                    }
                    else
                    {
                        var xishu = $(this).parent().parent().find("span[name*=lblGWXS]").html();
                        Zonghe += parseFloat(money);
                        ZongXiShu += parseFloat(xishu);
                    }
                }
                else {
                    $(this).parent().parent().find("input[name*=txtMoney]").attr("readonly", "readonly");
                }
                ZongZh += parseFloat(money);
            });
            $("#lblZongZH").html(ZongZh.toFixed(2));
            $("#lblZongheNew").html(Zonghe.toFixed(2));
            $("#<%= hidZongHeNew.ClientID %>").val(Zonghe.toFixed(2));
            $("#lblZongXS").html(ZongXiShu.toFixed(1));

        }
        
        function Calculate(){
            var yingfpe=0;
            var jieye=0;
            var zonge=0;
            var yfpe=$("#<%=tb_yfpe.ClientID %>").val();
            var jye=$("#<%=tb_jye.ClientID %>").val();
            yingfpe=parseFloat(yfpe);
            jieye=parseFloat(jye);
            zonge=yingfpe+jieye;
            $("#<%=txtZonghe.ClientID %>").val(zonge.toFixed(2));
        }
        
    </script>

    <script type="text/javascript">
        $(function() {
            $("#<%=txtHP.ClientID %>,#<%=txtLD.ClientID %>").keyup(function() {
            var scoreHp = $("#<%=txtHP.ClientID %>").val();
            var scoreDl = $("#<%=txtLD.ClientID%>").val();
                //     console.log($("#gr tr:not(:first):not(:last)"));
                $("#gr tr:not(:first):not(:last)").each(function() {
                    var hp = $(this).find("input[name*=txtScoreHP]").val();
                    var ld = $(this).find("input[name*=txtlScoreLD]").val();
                    var zongF = parseFloat(hp) * parseFloat(scoreHp) + parseFloat(ld) * parseFloat(scoreDl);
                    $(this).find("input[name*=txtZong]").val((zongF/100).toFixed(2));
                    $(this).find("input[name*=hidZong]").val((zongF/100).toFixed(2));
                });
            });
        })
    
    </script>

    <script type="text/javascript">
        $(function() {
            $("#gr input[name*=txtScoreHP],#gr input[name*=txtlScoreLD]").keyup(function() {
                var score = new Object();
                var $tr = $(this).parent().parent();
                score.hp = $tr.find("input[name*=txtScoreHP]").val();
                score.ld = $tr.find("input[name*=txtlScoreLD]").val();
                //                score.bl1 = $tr.find("span[name*=lblBL]").html();
                score.bl1 = $("#<%=txtHP.ClientID %>").val();
                score.bl2 = $("#<%=txtLD.ClientID %>").val();
                // score.zongf = $tr.find("span[name*=lblScoreZong]").html();
                score.zongf = score.hp * score.bl1 + score.ld * score.bl2;
                //  $tr.find("span[name*=lblScoreZong]").html((score.zongf / 100).toFixed(2));
                $tr.find("input[name*=txtZong]").val((score.zongf / 100).toFixed(2));
                $tr.find("input[name*=hidZong]").val((score.zongf / 100).toFixed(2));
                
                //    $tr.find("input[name*=txtScoreZong]").val(score.zongf / 100);
            });
        });
    
    </script>

    <script type="text/javascript">
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
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

            $('#win').dialog('close');
        }
       

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnSave" runat="server" Text="保  存" OnClick="btnSave_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAudit" runat="server" Text="提交审批" OnClick="btnAudit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_KaoHe_JXGZ_List.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="hidden" id="hidId" runat="server" />
        <input type="hidden" id="hidConext" runat="server" />
        <input type="hidden" id="hidState" runat="server" />
        <input type="hidden" id="hidAction" runat="server" />
        <input type="hidden" runat="server" id="hidZongHeNew" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer">
                        <div style="text-align: center;">
                            <h2>
                                部门绩效工资</h2>
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
                                        制单时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                    <td>
                                        选择制单年月：
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Width="100px" ID="txtKhNianYue" class="easyui-datebox"
                                            data-options="formatter:function(date){var y = date.getFullYear();var m = date.getMonth()+1;return y+'-'+m;},editable:false"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp; 月度公司绩效工资基数：<asp:Label ID="lblJS" runat="server" Width="60px"></asp:Label>
                                        <input type="hidden" id="hidJS" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table width="1150px">
                                <tr>
                                    <td>
                                        部门：
                                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        部门绩效工资应分配额：<asp:TextBox ID="tb_yfpe" Width="60px" runat="server" onfocus="this.blur()"></asp:TextBox>
                                    </td>
                                    <td>
                                        结余额：<asp:TextBox ID="tb_jye" Width="60px" runat="server" onkeyup="Calculate()" onblur="Calculate()"></asp:TextBox>
                                    </td>
                                    <td>
                                        部门绩效工资总和(不含部长)：
                                        <asp:TextBox ID="txtZonghe" Width="60px" runat="server" onfocus="this.blur()"></asp:TextBox>
                                        <input id="hidPerId" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div>
                            <asp:Button ID="btnCreatJx" runat="server" Text="初始化该月绩效工资列表" OnClick="btnCreatJx_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="删除行" OnClick="btnDelete_Click" />
                            <span id="spnBL" runat="server">比例：互评<asp:TextBox ID="txtHP" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                                领导评分<asp:TextBox ID="txtLD" runat="server" Width="30px" autocomplete="off"></asp:TextBox>
                             
                            </span>
                            <asp:Button ID="btnCacuJX" runat="server" Text="计算绩效工资(请先输入考核分数)" OnClick="btnCacuJX_Click" />
                            <span id="spnAddPer" runat="server">
                                <asp:TextBox ID="txtPer" runat="server" Width="80px" onchange="AddPerChange()"></asp:TextBox>
                                <asp:Button ID="btnAddPer" runat="server" Text="添加人员" OnClick="btnAddPer_Click" />
                                <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                    CompletionListCssClass="completionListElement" DelimiterCharacters="" Enabled="True"
                                    MinimumPrefixLength="1" ServiceMethod="get_st_id" ServicePath="~/Ajax.asmx" FirstRowSelected="true"
                                    TargetControlID="txtPer" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </span>
                        </div>
                        <br />
                        <asp:Panel ID="Panel2" runat="server">
                            <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                <asp:Repeater ID="Det_Repeater" runat="server">
                                    <HeaderTemplate>
                                        <tr class="tableTitle headcolor">
                                            <td align="center">
                                                <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <strong width="30px">姓名</strong>
                                            </td>
                                            <td align="center">
                                                <strong>部门</strong>
                                            </td>
                                            <td align="center">
                                                <strong>职位</strong>
                                            </td>
                                            <td align="center">
                                                <strong>岗位系数</strong>
                                            </td>
                                            <td align="center">
                                                <strong>部门月度成绩</strong>
                                            </td>
                                            <td align="center">
                                                <strong>互评分数</strong>
                                            </td>
                                            <td align="center">
                                                <strong>领导评分</strong>
                                            </td>
                                            <td align="center">
                                                <strong>人员月度成绩</strong>
                                            </td>
                                            <td align="center">
                                                <strong>比例</strong>
                                            </td>
                                            <td align="center">
                                                <strong>月度绩效工资</strong>
                                            </td>
                                            <td align="center">
                                                <strong>备注</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                                    Onclick="checkme(this)" />
                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                <input type="hidden" id="hidStId" value='<%#Eval("ST_ID") %>' runat="server" />
                                                <input type="hidden" id="hidMonth" value='<%#Eval("JxMonth") %>' runat="server" />
                                                <input type="hidden" id="hidId" value='<%#Eval("Id") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Width="50px" Text='<%#Eval("ST_NAME")  %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDEPNAME"  name="lblDEPNAME"  runat="server" Width="100px" Text='<%#Eval("DEP_NAME")  %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPosition" name="lblPosition" runat="server" Width="50px" Text='<%#Eval("PosName")  %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGWXS" name="lblGWXS" runat="server" Width="40px" Text='<%#Eval("GangWeiXiShu")  %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtDepartScore" runat="server" Width="50px" Text='<%#Eval("DepartScore")  %>' autocomplete="off"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtScoreHP" name="txtScoreHP" runat="server" Width="50px" Text='<%#Eval("Kh_ScoreHP")  %>' autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtlScoreLD" name="txtlScoreLD" runat="server" Width="50" Text='<%#Eval("Kh_ScoreLD")  %>' autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtZong" name="txtZong" runat="server" Width="50px" ReadOnly="true"
                                                    Text='<%#Eval("Kh_ScoreTotal") %>'></asp:TextBox>
                                                <input runat="server" name="hidZong" type="hidden" id="hidZong" value='<%#Eval("Kh_ScoreTotal") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBL" name="lblBL" runat="server" Width="50px" Text='<%#Eval("Kh_BL")  %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMoney" onkeyup="CaculateZong()" name="txtMoney" runat="server"
                                                    Width="100px" Text='<%#Eval("Money")  %>' autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNote" runat="server" Width="250px" Text='<%#Eval("Note") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <strong>合计：</strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        不含部长：
                                    </td>
                                    <td>
                                        <span id="lblZongXS"></span>
                                    </td>
                                    <td colspan="5" align="right">
                                        不含部长
                                    </td>
                                    <td>
                                        <span id="lblZongheNew"></span>
                                    </td>
                                    <td>
                                        含部长： <span id="lblZongZH"></span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审核信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-outer">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Panel runat="server" ID="Panel1">
                            <tr>
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
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
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult1" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
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
                        </asp:Panel>
                    </table>
                </div>
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
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
