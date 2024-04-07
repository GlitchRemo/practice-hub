(+ 2 3)
(* 2 3)
42
0.2
-0.2
2/4
"hello"
##Inf
nil
'(1 2 3)
[1 2 3]
#{1 2}
{:a 1 :b 2}
:alpha
(if (> 2 3) 1 2)
(if (< 2 3) 1 2)
(+ *1 *2)
(+)

(+' 10000000000000000000000000000000000000000 2)
(+ 10000000000000000000000000000000000000000 2)

;; defn and fn and def
(println "What is this" (+ 2 3))
(defn greet [name] (str "Hello " name))
(greet "riya")

(defn messanger 
  ([] (messanger "Hello world"))
  ([name] (str name)))

(messanger "Riya")
(messanger)

((fn [name] (str "name: " name)) "riya")
(defn greeting [name] (str "hello " name))
(def greeting1 (fn [name] (str "hello " name)))
(greeting "Bhombol")
(greeting1 "Bhombol")

;Anonymous fn
(#(str %1 %2 %&) 2 3 3 4 5)
(+ 2 3 4)

;;factorial
(defn fact [x] 
  (if (<= x 1) 1 
      (* x (fact (dec x)))) 
  )

(fact 3)





