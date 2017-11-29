<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_panel.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_panel"
    Title="物料代用管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
            height: 30px;
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
        }
        
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="../PC_Data/FixTable.css" rel="stylesheet" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
    
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>
    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        //        function RecordPostion()
        //        {
        //            //var div1 = obj;
        //            var div1=document.getElementById('dvrepeater');
        //            var sx = document.getElementById('dvscrollX');
        //            var sy = document.getElementById('dvscrollY');
        //            
        //            sy.value = div1.scrollTop;
        //            sx.value = div1.scrollLeft; 
        //        }
        //        
        //        function GetResultFromServer()
        //        {
        //            var sx = document.getElementById('dvscrollX');
        //            var sy = document.getElementById('dvscrollY');
        //            
        //            document.getElementById('dvrepeater').scrollTop = sy.value;
        //            document.getElementById('dvrepeater').scrollLeft = sx.value;               
        //        }
        //     self.moveTo(0,0)
        //     self.resizeTo(screen.availWidth,screen.availHeight)
        
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
        
        function xr4() {
            $("#hidPerson").val("person4");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=TextBoxp1.ClientID %>").val(r.st_name);
                $("#<%=TextBoxp1id.ClientID %>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=TextBoxp11.ClientID %>").val(r.st_name);
                $("#<%=TextBoxp11id.ClientID %>").val(r.st_id);
            }
            if (id == "person3") {
                $("#<%=TextBoxp2.ClientID %>").val(r.st_name);              
                $("#<%=TextBoxp2id.ClientID %>").val(r.st_id);
            }
            if (id == "person4") {
                $("#<%=TextBoxp3.ClientID %>").val(r.st_name);              
                $("#<%=TextBoxp3id.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        function ShowDetial(obj) {
            var tracknum = obj.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerText;
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("../TM_Data/TM_MarReplace_Detail.aspx?NoUse=" + time + "&tracknum=" + encodeURIComponent(tracknum), '', "dialogHeight:400px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td>
                                    代用单
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_SHR_INSERT" runat="server" Text="审核人填补" OnClick="btn_SHR_INSER" Visible="false" />
                                    <asp:Button ID="btn_THBZ" runat="server" Text="替换备注" OnClick="btn_THBZ_Click" />
                                    <asp:Button ID="btn_DYQR" runat="server" Text="打印确认" OnClick="btn_DYQR_Click" />
                                    <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click" Visible="false" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click"
                                        Enabled="false" />
                                    &nbsp;&nbsp;
                                    <%-- <asp:Button ID="btn_bohuiconcel" runat="server" Text="取消" OnClick="btn_bohuiconcel_Click" />&nbsp;&nbsp;&nbsp;--%><%--取消代用--%>
                                    <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" Enabled="false" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_Click" />
                                    &nbsp;&nbsp;
                                    <%--<asp:Button ID="btn_fanclose" runat="server" Text="反关闭"  onclick="btn_fanclose_Click" Visible="false"/>--%>
                                    <a href="javascript:history.go(-1);">向上一页</a>&nbsp;&nbsp;
                                    <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                        <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif"
                                            title="打印" /></asp:HyperLink>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="材料代用计划表" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_body1" runat="server">
                                    <div style="height: 480px; overflow: auto">
                                        <div class="fixbox2 xscroll" id="dvrepeater">
                                            <div>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="font-size: small; text-align: center;" colspan="4">
                                                            材料代用计划表
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 33%;" align="left">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单&nbsp;号:
                                                            <asp:TextBox ID="Tb_Code" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 33%;" align="left">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;期:
                                                            <asp:TextBox ID="Tb_Date" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td id="Td1" style="width: 34%;" align="left" runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;摘&nbsp;要:
                                                            <asp:TextBox ID="Tb_Abstract" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 33%;" align="left">
                                                            &nbsp;&nbsp;&nbsp;项&nbsp;目&nbsp;编&nbsp;号:
                                                            <asp:TextBox ID="Tb_pjid" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 33%;" align="left">
                                                            &nbsp;项&nbsp;目&nbsp;名&nbsp;称:
                                                            <asp:TextBox ID="Tb_pjname" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td id="Td2" style="width: 34%;" align="left" runat="server">
                                                            &nbsp;工&nbsp;程&nbsp;名&nbsp;称:
                                                            <asp:TextBox ID="Tb_engname" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table id="tab" class="nowrap fixtable fullwidth" align="center">
                                                <asp:Repeater ID="Marreplace_detail_repeater" runat="server" OnItemDataBound="Marreplace_detail_repeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                            <td colspan="2">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="ycljh">
                                                                <strong>原材料计划</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="dycljh">
                                                                <strong>代用材料计划</strong>
                                                            </td>
                                                            <td colspan="1">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum1" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length1" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width1" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum2" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length2" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width2" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit2" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" ondblclick="ShowDetial(this);">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUIGE" runat="server" Text='<%#Eval("marguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDCAIZHI" runat="server" Text='<%#Eval("marcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUOBIAO" runat="server" Text='<%#Eval("marguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDUNIT" runat="server" Text='<%#Eval("marcgunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNUMA" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum3" runat="server">
                                                                <asp:Label ID="MP_OLDNUMB" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="length3" runat="server">
                                                                <asp:Label ID="MP_OLDLENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width3" runat="server">
                                                                <asp:Label ID="MP_OLDWIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit3" runat="server">
                                                                <asp:Label ID="MP_FZUNIT" runat="server" Text='<%#Eval("fzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARID" runat="server" Text='<%#Eval("detailmarid")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARNAME" runat="server" Text='<%#Eval("detailmarnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUIGE" runat="server" Text='<%#Eval("detailmarguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWCAIZHI" runat="server" Text='<%#Eval("detailmarcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUOBIAO" runat="server" Text='<%#Eval("detailmarguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWUNIT" runat="server" Text='<%#Eval("detailmarunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNUMA" runat="server" Text='<%#Eval("detailmarnuma")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum4" runat="server">
                                                                <asp:Label ID="MP_NEWNUMB" runat="server" Text='<%#Eval("detailmarnumb")%>'></asp:Label>
                                                            </td>
                                                            <td id="length4" runat="server">
                                                                <asp:Label ID="MP_NEWLENGTH" runat="server" Text='<%#Eval("detaillength")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width4" runat="server">
                                                                <asp:Label ID="MP_NEWWIDTH" runat="server" Text='<%#Eval("detailwidth")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit4" runat="server">
                                                                <asp:Label ID="MP_NEWFZUNIT" runat="server" Text='<%#Eval("detailfzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="MP_OPTION" runat="server" Text='<%#Eval("alloption")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="27" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有数据！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <%--<asp:HiddenField ID="dvscrollX" runat="server" />
                                    <asp:HiddenField ID="dvscrollY" runat="server" />--%>
                                    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 25%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;负&nbsp;责&nbsp;人:
                                                    <asp:TextBox ID="tb_Manager" runat="server" Text=""></asp:TextBox>
                                                    <asp:Label ID="lb_ManagerID" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 25%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;技&nbsp;术&nbsp;员:
                                                    <asp:TextBox ID="tb_shenhe" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lb_shenheID" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 25%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;制&nbsp;单&nbsp;人:
                                                    <asp:TextBox ID="tb_Document" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lb_DocumentID" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 25%;" visible="false" align="left" runat="server" id="ckshr">
                                                    
                                                    <asp:TextBox ID="tb_ckshr" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:Label ID="tb_ckshrid" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="评审结果" TabIndex="1">
                            <ContentTemplate>
                                <div style="border: 1px solid #000000; height: 401px; overflow: auto">
                                    <asp:Panel ID="Pan_shenhe" runat="server">
                                        <table width="100%" style="border: 1px" cellpadding="2" cellspacing="0">
                                            <asp:Panel ID="Panel_shenhe1" runat="server">
                                                <asp:Panel ID="Panel_shen1body" runat="server" HorizontalAlign="Center" ForeColor="#FFCC00"
                                                    Enabled="false">
                                                    <tr>
                                                        <td>
                                                            意见:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="shenhe_note1" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                            <asp:Label ID="label1" Text="" runat="server" ></asp:Label>
                                                            <asp:TextBox ID="TextBoxp1" runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>
                                                            <asp:TextBox ID="TextBoxp1id" runat="server" style="display:none;"></asp:TextBox>
                                                            <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                                            onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            审核时间:
                                                            <asp:TextBox ID="TextBoxdatatime1" runat="server" Text="" CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="Panel_lead" Enabled="false">
                                                <tr>
                                                    <td>
                                                        审核意见:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="shenhe_lead" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        审核结论:
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rd_agree1" GroupName="shenhe1" runat="server" Text="同意" OnCheckedChanged="rd_agree5_checkedchanged"
                                                            AutoPostBack="true" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rd_disagree1" GroupName="shenhe1" runat="server" Text="拒绝" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                        部门领导:<asp:TextBox ID="TextBoxp11" runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>
                                                        <asp:TextBox ID="TextBoxp11id" runat="server" style="display:none;"></asp:TextBox>
                                                        <asp:Image runat="server" ID="imgSHR2" ImageUrl="../Assets/images/username_bg.gif"
                                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                        审核时间:<asp:TextBox ID="TextBoxdatatime11" runat="server" Text="" CssClass="text1style"
                                                            Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_shenhe2" runat="server">
                                                <asp:Panel ID="Panel_shenhe2body" runat="server" HorizontalAlign="Center" ForeColor="#FFCC00"
                                                    Enabled="false">
                                                    <tr>
                                                        <td>
                                                            审核意见:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="shenhe_note2" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rd_agree2" GroupName="shenhe2" runat="server" Text="同意" OnCheckedChanged="rd_agree2_checkedchanged"
                                                                AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rd_disagree2" GroupName="shenhe2" runat="server" Text="拒绝" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                            技术员:<asp:TextBox ID="TextBoxp2" runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>
                                                            <asp:TextBox ID="TextBoxp2id" runat="server" style="display:none;"></asp:TextBox>
                                                            <asp:Image runat="server" ID="imgSHR3" ImageUrl="../Assets/images/username_bg.gif"
                                                            onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            审核时间:<asp:TextBox ID="TextBoxdatatime2" runat="server" Text="" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_shenhe3" runat="server">
                                                <asp:Panel ID="Panel_shenhe3body" runat="server" HorizontalAlign="Center" ForeColor="#FFCC00"
                                                    Enabled="false">
                                                    <tr>
                                                        <td>
                                                            审核意见:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="shenhe_note3" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rd_agree3" GroupName="shenhe3" runat="server" Text="同意" TextAlign="Right"
                                                                OnCheckedChanged="rd_agree3_checkedchanged" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rd_disagree3" GroupName="shenhe3" runat="server" Text="拒绝" TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                            技术部负责人:<asp:TextBox ID="TextBoxp3" runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>
                                                            <asp:TextBox ID="TextBoxp3id" runat="server"  style="display:none;"></asp:TextBox>
                                                            <asp:Image runat="server" ID="imgSHR4" ImageUrl="../Assets/images/username_bg.gif"
                                                            onclick="xr4()" align="middle" Style="cursor: pointer" title="选择" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            审核时间:<asp:TextBox ID="TextBoxdatatime3" runat="server" Text="" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <%--<asp:Panel ID="Panel_shenhe4" runat="server">
                                                <asp:Panel ID="Panel_shenhe4body" runat="server" HorizontalAlign="Center" ForeColor="#FFCC00"
                                                    Enabled="false" Visible="false">
                                                    <tr>
                                                        <td>
                                                            审核意见:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="shenhe_note4" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rd_agree4" GroupName="shenhe4" runat="server" Text="同意" TextAlign="Right"
                                                                OnCheckedChanged="rd_agree4_checkedchanged" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rd_disagree4" GroupName="shenhe4" runat="server" Text="拒绝" TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                            储运部审核人:<asp:TextBox ID="TextBoxp4" runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>
                                                            <asp:TextBox ID="TextBoxp4id" runat="server" Visible="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            审核时间:<asp:TextBox ID="TextBoxdatatime4" runat="server" Text="" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </asp:Panel>--%>
                                        </table>
                                    </asp:Panel>
                                    
                                                                    
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
                                                保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                                                    onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                                                        data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
                                            <input id="hidPerson" type="hidden" value="" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
