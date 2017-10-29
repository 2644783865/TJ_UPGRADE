<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_BgypPcApply.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BgypPcApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增办公用品采购申请
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
    </style>
    <style type="text/css">
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
    
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="../PC_Data/FixTable.css" rel="stylesheet" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    
    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>
    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=SHR1id.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>
    
    <script type="text/javascript">
    $(function(){
    $('input[id$=WLNUM]').bind('blur',function(){
    //&& parseFloat($(this).val())<parseFloat($(this).parent().next().next().next().children().val())
    
    if ($(this).val() != ''&& parseFloat($(this).val())<=parseFloat($(this).parent().next().next().next().children().val())) {
      alert('提示：该物品可以直接申请使用，请进一步确认采购数量！');
    }

    });
    });
    </script>

    <script type="text/javascript">

        function autoCode(input) {
            var arrString = $(input).val();
            var $tr = $(input).parent().parent();
            if (arrString != null) {
                var array = arrString.split('|');
                //圆珠笔|蓝色|0.5|1-1-4-2|41
                $tr.find("input[name*=WLNAME]").val(array[0]);
                $tr.find("input[name*=WLMODEL]").val(array[1]);
                $tr.find("input[name*=WLPRICE]").val(array[2]);
                $tr.find("input[name*=WLUNIT]").val(array[3]);
                $tr.find("input[name*=WLBM]").val(array[4]);
                $tr.find("input[name*=WLCODE]").val(array[5]);
                $tr.find("input[name*=WLNUM]").val("");
                $tr.find("input[name*=WLJE]").val(array[2]);
                $tr.find("input[name*=num]").val(array[6]);

            }
        }
        function sum(input) {
        var zongji=0;
            var $tr = $(input).parent().parent();
            var dj = $tr.find("input[name*=WLPRICE]").val();
            var num = $tr.find("input[name*=WLNUM]").val();
            $tr.find("input[name*=WLJE]").val((dj * num).toFixed(2));
            
            $tr.parent().find("input[name*=WLJE]").each(function(){
            zongji+=parseFloat($(this).val());
            
            });
           $("#<%=lbljine.ClientID %>").html(zongji);
        }
    
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" OnClientClick="return checkPage();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="关闭" OnClick="close" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="申请采购明细" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="margin: 0px 0px 0px 10px">
                            <table width="90%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                <tr>
                                    <td style="width: 50%" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="addrow" runat="server" Text="增加行:"></asp:Label>
                                        <asp:TextBox ID="txtNum" runat="server" Width="50px" CssClass="center"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btninsert_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btndelete" runat="server" Text="删除行" OnClick="btndelete_Click" OnClientClick="return alert('确定删除此条目吗？')" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="2">
                                        办公用品采购申请单
                                        <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                            Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                                        <input type="text" id="InputColour" style="display: none" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;人：<asp:Label ID="LabelDoc"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;总&nbsp;&nbsp;&nbsp;额：<asp:Label ID="lbljine" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="center">
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="txt_note"
                                        runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox><asp:Label ID="state"
                                            runat="server" Visible="false"></asp:Label></tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                            <div style="width: 100%; margin: 0 auto">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                    CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="编码">
                                            <ItemTemplate>
                                                <asp:TextBox ID="WLBM" runat="server" Text='<%#Eval("WLBM") %>' onchange="autoCode(this)"
                                                    name="maId" ></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="marid_AutoCompleteExtender2" runat="server" CompletionSetCount="15"
                                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                                    ServicePath="~/OM_Data/Ajax.asmx" TargetControlID="WLBM" UseContextKey="True"
                                                    CompletionInterval="10">
                                                </asp:AutoCompleteExtender>
                                                <input id="CODE" runat="server" name="code" type="hidden" value='<%#Eval("CODE") %>' />
                                                <input id="WLCODE" runat="server" name="sId" type="hidden" value='<%#Eval("WLCODE") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="名称">
                                            <ItemTemplate>
                                                <asp:TextBox ID="WLNAME" name="name" runat="server" Width="80px" onchange="autoCode(this)"
                                                    Text='<%#Eval("WLNAME") %>'></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                                    ServicePath="~/OM_Data/Ajax.asmx" TargetControlID="WLNAME" UseContextKey="True"
                                                    CompletionInterval="10">
                                                </asp:AutoCompleteExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格型号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="WLMODEL" name="canshu" runat="server" style="border-style: none; width: 80px"
                                                    type="text" value='<%#Eval("WLMODEL") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="WLUNIT" name="unit" runat="server" style="border-style: none; width: 40px"
                                                    type="text" value='<%#Eval("WLUNIT") %>' onkeyup="grControlFocus(this)" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="数量" ItemStyle-BackColor="#FFFFCC" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="WLNUM" name="num" runat="server" style="border-style: none; width: 80px"
                                                    type="text" value='<%#Eval("WLNUM") %>' onkeyup="sum(this)" />
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#FFFFCC" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="WLPRICE" name="dj" runat="server" style="border-style: none; width: 80px"
                                                    type="text" value='<%#Eval("WLPRICE") %>' onkeyup="sum(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="WLJE" name="je" runat="server" style="border-style: none; width: 80px"
                                                    readonly="readonly" type="text" value='<%#Eval("WLJE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存数量" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="num" name="num_store" runat="server" style="border-style: none; width: 80px"
                                                    readonly="readonly" type="text" value='<%#Eval("num") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请部门" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input id="txtDep" name="dep_name" runat="server" style="border-style: none; width: 80px"
                                                    type="text" value='<%#Eval("DEPNAME") %>' />        
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                             <%--   <input id="txtNote" name="num_store" runat="server" style="border-style: none; width: 80px"
                                                    type="text" value='<%#Eval("Note") %>' />--%>
                                                    
                                                      <asp:TextBox runat="server" TextMode="MultiLine" Width="150px" ID="txtNote" Text='<%#Eval("Note") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLNOTE" runat="server" style="border-style: none; width: 200px" type="text"
                                        value='<%#Eval("WLNOTE") %>' onkeyup="grControlFocus(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" onfocus="this.blur()" Width="120px"></asp:TextBox>
                                                <asp:TextBox ID="SHR1id" runat="server" style="display:none;"></asp:TextBox>
                                                <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                                        onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="2"></asp:ListItem>
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
                                                <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                        
                        </table>
                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <input id="Hidden1" type="hidden" value="" />
                    </div>
                </div>
                
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
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
