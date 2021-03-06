using System.Collections.Generic;
using Unity.DocZh.Utility;
using Unity.UIWidgets.gestures;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace Unity.DocZh.Components
{
    public partial class ScriptingContent
    {
        public class Entrance : StatefulWidget
        {
            public override State createState() => new EntranceState();

            private static readonly TextStyle EntranceNormalTextStyle = new TextStyle(
                fontSize: 16,
                color: new Color(0xff212121),
                fontFamily: "PingFang",
                height: 1.5f.LineHeight()
            );

            private static readonly TextStyle EntranceBoldTextStyle = new TextStyle(
                fontWeight: FontWeight.w500
            );

            private static readonly TextStyle EntranceLinkTextStyle = new TextStyle(
                color: new Color(0xfff32194),
                decoration: TextDecoration.underline
            );

            private class EntranceState : State<Entrance>
            {
                private TapGestureRecognizer _scriptingSectionRecognizer;

                public override void initState()
                {
                    base.initState();
                    _scriptingSectionRecognizer = new TapGestureRecognizer
                    {
                        onTap = () => { LocationUtil.Go("/Manual/ScriptingSection"); }
                    };
                }

                public override void dispose()
                {
                    _scriptingSectionRecognizer?.dispose();
                    base.dispose();
                }

                public override Widget build(BuildContext buildContext)
                {
                    var container = new Container(
                        padding: EdgeInsets.only(right: 64f, top: 32, bottom: 32f),
                        child: new Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: new List<Widget>
                            {
                                new Text(
                                    "???????????? Unity Scripting Reference?????????????????????",
                                    style: new TextStyle(
                                        fontSize: 36,
                                        height: 1.16666666667f.LineHeight(),
                                        color: new Color(0xff212121)
                                    )
                                ),
                                new Container(
                                    margin: EdgeInsets.only(top: 36),
                                    child: new RichText(
                                        text: new TextSpan(
                                            children: new List<TextSpan>
                                            {
                                                new TextSpan(
                                                    "???????????????????????? Unity ??????????????? API ????????????????????????????????????????????????????????? Unity ???????????????????????????????????????????????????????????????"),
                                                new TextSpan(
                                                    " Scripting??????????????????",
                                                    style: EntranceLinkTextStyle,
                                                    recognizer: _scriptingSectionRecognizer
                                                ),
                                                new TextSpan("????????????????????????")
                                            },
                                            style: EntranceNormalTextStyle
                                        )
                                    )
                                ),
                                new Container(
                                    margin: EdgeInsets.only(top: 24),
                                    child: new Text(
                                        "???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????",
                                        style: EntranceNormalTextStyle
                                    )
                                ),
                                new Container(
                                    margin: EdgeInsets.only(top: 24),
                                    child: new RichText(
                                        text: new TextSpan(
                                            children: new List<TextSpan>
                                            {
                                                new TextSpan(
                                                    "?????????????????????????????????????????????????????????????????????????????????????????????????????????????????? Unity????????????????????????????????????????????????????????? "),
                                                new TextSpan(
                                                    "C#",
                                                    style: EntranceBoldTextStyle
                                                ),
                                                new TextSpan(" ??? "),
                                                new TextSpan(
                                                    "JavaScript",
                                                    style: EntranceBoldTextStyle
                                                ),
                                                new TextSpan(" ??????????????????????????????????????????????????????????????????API ???????????????????????????????????????????????????????????????????????????")
                                            },
                                            style: EntranceNormalTextStyle
                                        )
                                    )
                                ),

                                new Container(
                                    margin: EdgeInsets.symmetric(vertical: 24),
                                    child: new RichText(
                                        text: new TextSpan(
                                            children: new List<TextSpan>
                                            {
                                                new TextSpan("API ??????????????? namespace ???????????????????????????????????????????????????????????????????????????"),
                                                new TextSpan("UnityEngine"),
                                                new TextSpan("????????????????????????????????????")
                                            },
                                            style: EntranceNormalTextStyle
                                        )
                                    )
                                ),
                            }
                        )
                    );
                    return new Scroller(
                        child: new SingleChildScrollView(
                            child: new ScrollableOverlay(
                                child: new Container(
                                    padding: EdgeInsets.only(right: 48f),
                                    child: new Column(
                                        crossAxisAlignment: CrossAxisAlignment.stretch,
                                        children: new List<Widget>
                                        {
                                            new Container(
                                                constraints: new BoxConstraints(
                                                    minHeight: MediaQuery.of(context).size.height - Header.Height -
                                                               SearchBar.Height - Footer.Height),
                                                child: container
                                            ),
                                            new Footer(style: Footer.Light, showSocials: false)
                                        }
                                    )
                                )
                            )
                        )
                    );
                }
            }
        }
    }
}